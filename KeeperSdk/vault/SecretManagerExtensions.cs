﻿using Google.Protobuf;
using KeeperSecurity.Authentication;
using KeeperSecurity.Utils;
using Records;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AuthProto = Authentication;

namespace KeeperSecurity.Vault
{
    public partial class VaultOnline : ISecretManager
    {
        /// <inheritdoc/>
        public async Task<SecretsManagerApplication> GetSecretManagerApplication(string recordUid, bool force = false)
        {
            if (!TryGetKeeperApplication(recordUid, out var ar))
            {
                return null;
            }

            if (!force && ar is SecretsManagerApplication ksma)
            {
                return ksma;
            }

            var applicationUid = ar.Uid.Base64UrlDecode();
            var rq = new AuthProto.GetAppInfoRequest();
            rq.AppRecordUid.Add(ByteString.CopyFrom(applicationUid));

            var rs = await Auth.ExecuteAuthRest<AuthProto.GetAppInfoRequest, AuthProto.GetAppInfoResponse>("vault/get_app_info", rq);
            var appInfo = rs.AppInfo.FirstOrDefault(x => x.AppRecordUid.SequenceEqual(applicationUid));
            var application = new SecretsManagerApplication
            {
                Uid = ar.Uid,
                Revision = ar.Revision,
                ClientModified = ar.ClientModified,
                Title = ar.Title,
                Type = ar.Type,
                Version = ar.Version,
                Owner = ar.Owner,
                Shared = ar.Shared,
                RecordKey = ar.RecordKey,
                IsExternalShare = appInfo.IsExternalShare,
                Devices = appInfo.Clients.Select(x => new SecretsManagerDevice
                {
                    Name = x.Id,
                    DeviceId = x.ClientId.ToByteArray().Base64UrlEncode(),
                    CreatedOn = DateTimeOffsetExtensions.FromUnixTimeMilliseconds(x.CreatedOn),
                    FirstAccess = x.FirstAccess > 0 ? DateTimeOffsetExtensions.FromUnixTimeMilliseconds(x.FirstAccess) : (DateTimeOffset?) null,
                    LastAccess = x.LastAccess > 0 ? DateTimeOffsetExtensions.FromUnixTimeMilliseconds(x.LastAccess) : (DateTimeOffset?) null,
                    LockIp = x.LockIp,
                    IpAddress = x.IpAddress,
                    PublicKey = x.PublicKey.ToByteArray(),
                    FirstAccessExpireOn = x.FirstAccessExpireOn > 0 ? DateTimeOffsetExtensions.FromUnixTimeMilliseconds(x.FirstAccessExpireOn) : (DateTimeOffset?) null,
                    AccessExpireOn = x.AccessExpireOn > 0 ? DateTimeOffsetExtensions.FromUnixTimeMilliseconds(x.AccessExpireOn) : (DateTimeOffset?) null,
                }).ToArray(),
                Shares = appInfo.Shares
                .Where(x =>
                {
                    var uid = x.SecretUid.ToByteArray().Base64UrlEncode();
                    if (x.ShareType == AuthProto.ApplicationShareType.ShareTypeRecord)
                    {
                        return TryGetKeeperRecord(uid, out _);
                    }
                    else
                    {
                        return TryGetSharedFolder(uid, out _);
                    }
                })
                .Select(x => new SecretManagerShare
                {
                    SecretUid = x.SecretUid.ToByteArray().Base64UrlEncode(),
                    SecretType = (SecretManagerSecretType) x.ShareType,
                    Editable = x.Editable,
                    CreatedOn = DateTimeOffsetExtensions.FromUnixTimeMilliseconds(x.CreatedOn)
                }).ToArray()
            };

            keeperApplications.TryAdd(application.Uid, application);

            return application;
        }

        /// <inheritdoc/>
        public async Task<ApplicationRecord> CreateSecretManagerApplication(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new KeeperInvalidParameter("CreateSecretManagerApplication", "title", "", "Application Title cannot be empty");
            }
            var data = new RecordApplicationData
            {
                Title = title,
                Type = "app"
            };
            var appUid = CryptoUtils.GenerateUid();
            var appKey = CryptoUtils.GenerateEncryptionKey();
            var dataBytes = JsonUtils.DumpJson(data);
            var rq = new ApplicationAddRequest
            {
                AppUid = ByteString.CopyFrom(appUid.Base64UrlDecode()),
                RecordKey = ByteString.CopyFrom(CryptoUtils.EncryptAesV2(appKey, Auth.AuthContext.DataKey)),
                Data = ByteString.CopyFrom(CryptoUtils.EncryptAesV2(dataBytes, appKey)),
                ClientModifiedTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            };
            await Auth.ExecuteAuthRest("vault/application_add", rq);
            await ScheduleSyncDown(TimeSpan.FromSeconds(0));
            if (TryGetKeeperApplication(appUid, out var ar))
            {
                return ar;
            }
            return null;
        }

        /// <inheritdoc/>
        public async Task DeleteSecretManagerApplication(string applicationId)
        {
            await this.DeleteVaultObjects(new[] { new RecordPath { RecordUid = applicationId } }, true);
        }


        /// <inheritdoc/>
        public async Task<SecretsManagerApplication> ShareToSecretManagerApplication(string applicationId, string sharedFolderOrRecordUid, bool editable)
        {
            if (!TryGetKeeperApplication(applicationId, out var application))
            {
                throw new KeeperInvalidParameter("ShareToSecretManagerApplication", "applicationId", applicationId, "Application not found");
            }

            var isRecord = false;
            byte[] secretKey = null;
            if (TryGetSharedFolder(sharedFolderOrRecordUid, out var sf))
            {
                secretKey = sf.SharedFolderKey;
            }
            else if (TryGetKeeperRecord(sharedFolderOrRecordUid, out var r))
            {
                if (r is PasswordRecord || r is TypedRecord)
                {
                    isRecord = true;
                    secretKey = r.RecordKey;
                }
                else
                {
                    throw new KeeperInvalidParameter("ShareToSecretManagerApplication", "sharedFolderOrRecordUid", sharedFolderOrRecordUid, "Invalid record type");
                }
            }
            else
            {
                throw new KeeperInvalidParameter("ShareToSecretManagerApplication", "sharedFolderOrRecordUid", sharedFolderOrRecordUid, "Shared folder or Record do not exist");
            }
            var addRq = new AuthProto.AppShareAdd
            {
                SecretUid = ByteString.CopyFrom(sharedFolderOrRecordUid.Base64UrlDecode()),
                ShareType = isRecord ? AuthProto.ApplicationShareType.ShareTypeRecord : AuthProto.ApplicationShareType.ShareTypeFolder,
                EncryptedSecretKey = ByteString.CopyFrom(CryptoUtils.EncryptAesV2(secretKey, application.RecordKey)),
                Editable = editable,
            };
            var rq = new AuthProto.AddAppSharesRequest
            {
                AppRecordUid = ByteString.CopyFrom(application.Uid.Base64UrlDecode())
            };
            rq.Shares.Add(addRq);
            await Auth.ExecuteAuthRest("vault/app_share_add", rq);
            return await GetSecretManagerApplication(application.Uid, true);
        }

        /// <inheritdoc/>
        public async Task<SecretsManagerApplication> UnshareFromSecretManagerApplication(string applicationId, string sharedFolderOrRecordUid)
        {
            if (!TryGetKeeperApplication(applicationId, out var application))
            {
                throw new KeeperInvalidParameter("UnshareFromSecretManagerApplication", "applicationId", applicationId, "Application not found");
            }

            var rq = new AuthProto.RemoveAppSharesRequest
            {
                AppRecordUid = ByteString.CopyFrom(application.Uid.Base64UrlDecode())
            };
            var uidBytes = sharedFolderOrRecordUid.Base64UrlDecode();
            if (uidBytes.Length > 0)
            {
                rq.Shares.Add(ByteString.CopyFrom(uidBytes));
            }

            await Auth.ExecuteAuthRest("vault/app_share_remove", rq);

            return await GetSecretManagerApplication(application.Uid, true);
        }


        /// <inheritdoc/>
        public async Task<Tuple<SecretsManagerDevice, string>> AddSecretManagerClient(
            string applicationId, bool? unlockIp = null, int? firstAccessExpireInMinutes = null,
            int? accessExpiresInMinutes = null, string name = null)
        {
            if (!TryGetKeeperApplication(applicationId, out var application))
            {
                throw new KeeperInvalidParameter("AddSecretManagerClient", "applicationId", applicationId, "Application not found");
            }

            var clientKey = CryptoUtils.GenerateEncryptionKey();
            var hash = new HMACSHA512(clientKey);
            var clientId = hash.ComputeHash(Encoding.UTF8.GetBytes("KEEPER_SECRETS_MANAGER_CLIENT_ID"));

            var encryptedAppKey = CryptoUtils.EncryptAesV2(application.RecordKey, clientKey);

            var rq = new AuthProto.AddAppClientRequest
            {
                AppRecordUid = ByteString.CopyFrom(application.Uid.Base64UrlDecode()),
                EncryptedAppKey = ByteString.CopyFrom(encryptedAppKey),
                ClientId = ByteString.CopyFrom(clientId),
                LockIp = unlockIp != null ? !unlockIp.Value : true,
                FirstAccessExpireOn = DateTimeOffset.UtcNow.AddMinutes(
                    firstAccessExpireInMinutes != null ? firstAccessExpireInMinutes.Value : 60).ToUnixTimeMilliseconds(),
            };
            if (accessExpiresInMinutes.HasValue)
            {
                rq.AccessExpireOn = DateTimeOffset.UtcNow.AddMinutes(accessExpiresInMinutes.Value).ToUnixTimeMilliseconds();
            }
            if (!string.IsNullOrEmpty(name))
            {
                rq.Id = name;
            }

            await Auth.ExecuteAuthRest("vault/app_client_add", rq);
            var appDetails = await GetSecretManagerApplication(application.Uid, true);
            var client = clientId.Base64UrlEncode();
            var device = appDetails.Devices.FirstOrDefault(x => x.DeviceId == client);
            if (device == null)
            {
                throw new Exception($"Client Error");
            }

            var host = Auth.Endpoint.Server;
            switch (host)
            {
                case "keepersecurity.com":
                    host = "US";
                    break;
                case "keeperseurity.eu":
                    host = "EU";
                    break;
                case "keepersecurity.com.au":
                    host = "AU";
                    break;
                case "govcloud.keepersecurity.us":
                    host = "GOV";
                    break;
            }
            return Tuple.Create(device, $"{host}:{clientKey.Base64UrlEncode()}");
        }

        /// <inheritdoc/>
        public async Task DeleteSecretManagerClient(string applicationId, string deviceId)
        {
            if (!TryGetKeeperApplication(applicationId, out var application))
            {
                throw new KeeperInvalidParameter("RemoveSecretManagerClient", "applicationId", applicationId, "Application not found");
            }

            var rq = new AuthProto.RemoveAppClientsRequest
            {
                AppRecordUid = ByteString.CopyFrom(application.Uid.Base64UrlDecode()),
            };
            var clientBytes = deviceId.Base64UrlDecode();
            if (clientBytes.Length > 0)
            {
                rq.Clients.Add(ByteString.CopyFrom(clientBytes));
            }

            await Auth.ExecuteAuthRest("vault/app_client_remove", rq);
            await GetSecretManagerApplication(application.Uid, true);
        }
    }
}
