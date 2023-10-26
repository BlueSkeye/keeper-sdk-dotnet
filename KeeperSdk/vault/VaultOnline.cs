using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using KeeperSecurity.Authentication;
using KeeperSecurity.Commands;
using KeeperSecurity.Utils;
using Records;
using AuthProto = Authentication;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents Keeper Vault connected to Keeper server.
    /// </summary>
    public partial class VaultOnline : VaultData, IVault, IVaultFileAttachment,
        ISecretManager, IVaultSharedFolder
    {
        /// <summary>
        /// Instantiate <see cref="VaultOnline"/> instance.
        /// </summary>
        /// <param name="auth">Keeper authentication.</param>
        /// <param name="storage">Keeper offline storage.</param>
        public VaultOnline(IAuthentication auth, IKeeperStorage storage = null)
            : base(auth.AuthContext.ClientKey, storage ?? new InMemoryKeeperStorage())
        {
            Auth = auth;
        }

        /// <summary>
        /// Gets Keeper authentication.
        /// </summary>
        public IAuthentication Auth { get; }

        private bool _autoSync;

        /// <summary>
        /// Gets or sets vault auto sync flag.
        /// </summary>
        /// <remarks>When <c>True</c> the library subscribes to the Vault change notifications.</remarks>
        public bool AutoSync
        {
            get => _autoSync;
            set
            {
                _autoSync = value && Auth.PushNotifications != null;
                if (_autoSync)
                {
                    Auth.PushNotifications?.RegisterCallback(OnNotificationReceived);
                }
                else
                {
                    Auth.PushNotifications?.RemoveCallback(OnNotificationReceived);
                }
            }
        }

        /// <exclude />
        public bool RecordTypesLoaded { get; set; } 

        /// <summary>
        /// Gets User Interaction interface.
        /// </summary>
        public IVaultUi VaultUi { get; set; }

        private long _scheduledAt;
        private Task _syncDownTask;

        /// <summary>
        /// Schedules sync down.
        /// </summary>
        /// <param name="delay">delay</param>
        /// <returns>Awaitable task</returns>
        public Task ScheduleSyncDown(TimeSpan delay)
        {
            if (delay > TimeSpan.FromSeconds(5))
            {
                delay = TimeSpan.FromSeconds(5);
            }

            var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            if (_syncDownTask != null && _scheduledAt > now)
            {
                if (now + (long) delay.TotalMilliseconds < _scheduledAt)
                {
                    return _syncDownTask;
                }
            }

            Task myTask = null;
            myTask = Task.Run(async () =>
            {
                try
                {
                    if (delay.TotalMilliseconds > 10)
                    {
                        await Task.Delay(delay);
                    }

                    if (myTask == _syncDownTask)
                    {
                        _scheduledAt = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000;
                        await this.RunSyncDown();
                    }
                }
                finally
                {
                    if (myTask == _syncDownTask)
                    {
                        _syncDownTask = null;
                        _scheduledAt = 0;
                    }
                }
            });
            _scheduledAt = now + (long) delay.TotalMilliseconds;
            _syncDownTask = myTask;
            return myTask;
        }

        /// <summary>
        /// Immediately executes sync down.
        /// </summary>
        /// <returns>Awaitable task</returns>
        public async Task SyncDown()
        {
            await ScheduleSyncDown(TimeSpan.FromMilliseconds(10));
        }

        internal bool OnNotificationReceived(NotificationEvent evt)
        {
            if (evt != null & (evt?.Event == "sync" || evt?.Event == "sharing_notice"))
            {
                if (evt.Sync)
                {
                    ScheduleSyncDown(TimeSpan.FromSeconds(5));
                }
            }

            return false;
        }

        protected override void Dispose(bool disposing)
        {
            Auth.PushNotifications?.RemoveCallback(OnNotificationReceived);
            base.Dispose(disposing);
        }

        /// <inheritdoc/>
        public void AuditLogRecordOpen(string recordUid)
        {
            _ = Task.Run(async () =>
            {
                await Auth.AuditEventLogging("open_record", new AuditEventInput { RecordUid = recordUid });
            });
        }

        /// <inheritdoc/>
        public void AuditLogRecordCopyPassword(string recordUid)
        {
            _ = Task.Run(async () =>
            {
                await Auth.AuditEventLogging("copy_password", new AuditEventInput { RecordUid = recordUid });
            });
        }


        /// <inheritdoc/>
        public Task<KeeperRecord> CreateRecord(KeeperRecord record, string folderUid = null)
        {
            return this.AddRecordToFolder(record, folderUid);
        }

        /// <inheritdoc/>
        public Task<KeeperRecord> UpdateRecord(KeeperRecord record, bool skipExtra = true)
        {
            return this.PutRecord(record, skipExtra);
        }

        /// <inheritdoc/>
        public Task StoreNonSharedData<T>(string recordUid, T nonSharedData) where T : RecordNonSharedData, new()
        {
            return this.PutNonSharedData(recordUid, nonSharedData);
        }

        /// <inheritdoc/>
        public Task DeleteRecords(RecordPath[] records)
        {
            foreach (var path in records)
            {
                if (string.IsNullOrEmpty(path.RecordUid))
                {
                    throw new VaultException($"Record UID cannot be empty");
                }

                var folder = this.GetFolder(path.FolderUid);
                if (!folder.Records.Contains(path.RecordUid))
                {
                    throw new VaultException($"Record {path.RecordUid} not found in the folder {folder.Name}");
                }
            }

            return this.DeleteVaultObjects(records);
        }

        /// <inheritdoc/>
        public async Task MoveRecords(RecordPath[] records, string dstFolderUid, bool link = false)
        {
            foreach (var path in records)
            {
                if (string.IsNullOrEmpty(path.RecordUid)) continue;

                var srcFolder = this.GetFolder(path.FolderUid);
                if (srcFolder.Records.All(x => x != path.RecordUid))
                {
                    throw new VaultException($"Record {path.RecordUid} not found in the folder {srcFolder.Name} ({srcFolder.FolderUid})");
                }
            }

            var dstFolder = this.GetFolder(dstFolderUid);
            await this.MoveToFolder(records, dstFolder.FolderUid, link);
        }

        /// <inheritdoc/>
        public async Task MoveFolder(string srcFolderUid, string dstFolderUid, bool link = false)
        {
            var srcFolder = this.GetFolder(srcFolderUid);
            var dstFolder = this.GetFolder(dstFolderUid);

            await this.MoveToFolder(new[] {new RecordPath {FolderUid = srcFolder.FolderUid}}, dstFolder.FolderUid, link);
        }

        /// <inheritdoc/>
        public Task<FolderNode> CreateFolder(string folderName, string parentFolderUid = null, SharedFolderOptions sharedFolderOptions = null)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                throw new VaultException("Folder name cannot be empty");
            }

            var parent = this.GetFolder(parentFolderUid);
            var nameExists = parent.Subfolders
                .Select(x => this.TryGetFolder(x, out var v) ? v : null)
                .Any(x => x != null && string.Compare(x.Name, folderName, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (nameExists)
            {
                throw new VaultException($"Folder with name {folderName} already exists in {parent.Name}");
            }


            return this.AddFolder(folderName, parentFolderUid, sharedFolderOptions);
        }

        /// <inheritdoc/>
        public Task<FolderNode> RenameFolder(string folderUid, string newName)
        {
            var folder = this.GetFolder(folderUid);
            if (folder == null)
            {
                throw new VaultException($"Folder \"{folderUid}\" does not exist");
            }

            return this.FolderUpdate(folder.FolderUid, newName);
        }

        /// <inheritdoc/>
        public Task<FolderNode> UpdateFolder(string folderUid, string newName, SharedFolderOptions sharedFolderOptions = null)
        {
            var folder = this.GetFolder(folderUid);
            if (folder == null)
            {
                throw new VaultException($"Folder \"{folderUid}\" does not exist");
            }

            return this.FolderUpdate(folder.FolderUid, newName, sharedFolderOptions);
        }

        /// <inheritdoc/>
        public Task DeleteFolder(string folderUid)
        {
            var folder = this.GetFolder(folderUid);
            if (string.IsNullOrEmpty(folder.FolderUid))
            {
                throw new VaultException("Cannot delete the root folder");
            }

            return this.DeleteVaultObjects(Enumerable.Repeat(new RecordPath
                {
                    FolderUid = folder.FolderUid,
                },
                1));
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TeamInfo>> GetTeamsForShare()
        {
            var request = new GetAvailableTeamsCommand();
            var response = await Auth.ExecuteAuthCommand<GetAvailableTeamsCommand, GetAvailableTeamsResponse>(request);
            return response.teams.Select(x => new TeamInfo
            {
                TeamUid = x.teamUid,
                Name = x.teamName,
            });
        }

        /// <inheritdoc/>
        public async Task<ShareWithUsers> GetUsersForShare()
        {
            var request = new GetShareAutoCompleteCommand();
            var response = await Auth.ExecuteAuthCommand<GetShareAutoCompleteCommand, GetShareAutoCompleteResponse>(request);
            return new ShareWithUsers
            {
                SharesWith = response.SharesWithUsers?.Select(x => x.Email).ToArray() ?? new string [0],
                SharesFrom = response.SharesFromUsers?.Select(x => x.Email).ToArray() ?? new string[0],
                GroupUsers = response.GroupUsers?.Select(x => x.Email).ToArray() ?? new string[0]
            };
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RecordSharePermissions>> GetSharesForRecords(IEnumerable<string> recordUids)
        {
            var permissions = new List<RecordSharePermissions>();

            var records = new List<RecordAccessPath>();
            foreach (var recordUid in recordUids)
            {
                if (TryGetKeeperRecord(recordUid, out var record))
                {
                    if (record.Shared)
                    {
                        var rap = new RecordAccessPath
                        {
                            RecordUid = recordUid
                        };
                        this.ResolveRecordAccessPath(rap);
                        records.Add(rap);
                    }
                    else
                    {
                        permissions.Add(new RecordSharePermissions
                        {
                            RecordUid = recordUid,
                            UserPermissions = new[] { new UserRecordPermissions {
                                Username = Auth.Username,
                                Owner = true,
                                CanEdit = true,
                                CanShare = true
                            } }
                        });
                    }
                }
            }
            if (records.Count == 0)
            {
                return Enumerable.Empty<RecordSharePermissions>();
            }
            var rq = new GetRecordsCommand
            {
                Include = new[] { "shares" },
                Records = records.ToArray()
            };

            var rs = await Auth.ExecuteAuthCommand<GetRecordsCommand, GetRecordsResponse>(rq);

            permissions.AddRange(rs.Records?.Select(x =>
            {
                return new RecordSharePermissions
                {
                    RecordUid = x.RecordUid,
                    UserPermissions = x.UserPermissions?.Select(y => new UserRecordPermissions
                    {
                        Username = y.Username,
                        Owner = y.Owner,
                        CanEdit = y.Editable,
                        CanShare = y.Sharable,
                        AwaitingApproval = y.AwaitingApproval
                    }).ToArray(),
                    SharedFolderPermissions = x.SharedFolderPermissions?.Select(y => new SharedFolderRecordPermissions
                    {
                        SharedFolderUid = y.SharedFolderUid,
                        CanEdit = y.Editable,
                        CanShare = y.Reshareable,
                    }).ToArray()
                };
            }));

            return permissions;
        }

        /// <inheritdoc/>
        public async Task CancelSharesWithUser(string username) {
            var rq = new CancelShareCommand
            {
                FromEmail = Auth.Username,
                ToEmail = username
            };

            await Auth.ExecuteAuthCommand(rq);
        }

        /// <inheritdoc/>
        public async Task<Tuple<byte[], byte[]>> GetUserPublicKeys(string username)
        {
            var pkRq = new AuthProto.GetPublicKeysRequest();
            pkRq.Usernames.Add(username);

            var pkRss = await Auth.ExecuteAuthRest<AuthProto.GetPublicKeysRequest, AuthProto.GetPublicKeysResponse>("vault/get_public_keys", pkRq);
            var pkRs = pkRss.KeyResponses.FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase));
            if (pkRs == null)
            {
                throw new KeeperApiException("no_user_in_response", "User cannot be found");
            }
            if (!string.IsNullOrEmpty(pkRs.ErrorCode))
            {
                if (string.Equals(pkRs.ErrorCode, "no_active_share_exist"))
                {
                    throw new NoActiveShareWithUserException(username, pkRs.ErrorCode, pkRs.Message);
                }
                else
                {
                    throw new KeeperApiException(pkRs.ErrorCode, pkRs.Message);
                }
            }

            return Tuple.Create(pkRs.PublicKey?.ToByteArray(), pkRs.PublicEccKey?.ToByteArray());
        }

        /// <inheritdoc/>
        public async Task SendShareInvitationRequest(string username)
        {
            var inviteRq = new AuthProto.SendShareInviteRequest
            {
                Email = username
            };
            await Auth.ExecuteAuthRest("vault/send_share_invite", inviteRq);
        }

        /// <inheritdoc/>
        public async Task ShareRecordWithUser(string recordUid, string username, bool? canReshare, bool? canEdit)
        {
            if (!TryGetKeeperRecord(recordUid, out var record))
            {
                throw new KeeperApiException("not_found", "Record not found");
            }

            var recordShares = (await GetSharesForRecords(new[] { recordUid})).FirstOrDefault(x => x.RecordUid == recordUid);

            var targetPermission = recordShares?.UserPermissions
                .FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase));

            var rsuRq = new RecordShareUpdateCommand();
            var ro = new RecordShareObject
            {
                ToUsername = username,
                RecordUid = recordUid,
            };
            this.ResolveRecordAccessPath(ro, forShare: true);

            if (targetPermission == null)
            {
                var keyTuple = await GetUserPublicKeys(username);
                var rsaKey = keyTuple.Item1;
                var ecKey = keyTuple.Item2;
                var useEcKey = ecKey != null && record?.Version != 2;
                if (useEcKey)
                {
                    var pk = CryptoUtils.LoadPublicEcKey(ecKey);
                    ro.RecordKey = CryptoUtils.EncryptEc(record.RecordKey, pk).Base64UrlEncode();
                    ro.useEccKey = true;
                }
                else
                {
                    var pk = CryptoUtils.LoadPublicKey(rsaKey);
                    ro.RecordKey = CryptoUtils.EncryptRsa(record.RecordKey, pk).Base64UrlEncode();
                }
                ro.Shareable = canReshare ?? false;
                ro.Editable = canEdit ?? false;

                rsuRq.AddShares = new[] { ro };
            }
            else
            {
                ro.Shareable = canReshare ?? targetPermission.CanShare;
                ro.Editable = canEdit ?? targetPermission.CanEdit;

                rsuRq.UpdateShares = new[] { ro };
            }

            var rsuRs = await Auth.ExecuteAuthCommand<RecordShareUpdateCommand, Commands.RecordShareUpdateResponse>(rsuRq);
            var statuses = targetPermission == null ? rsuRs.AddStatuses : rsuRs.UpdateStatuses;
            if (statuses != null)
            {
                var status = statuses.FirstOrDefault(x => string.Equals(x.RecordUid, recordUid) && string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase));
                if (status != null && !string.Equals(status.Status, "success", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new KeeperApiException(status.Status, status.Message);
                }
            }
        }


        /// <inheritdoc/>
        public async Task TransferRecordToUser(string recordUid, string username)
        {
            if (!TryGetKeeperRecord(recordUid, out var record))
            {
                throw new KeeperApiException("not_found", "Record not found");
            }
            if (!record.Owner)
            {
                throw new KeeperApiException("not_owner", "Only record owner can transfer ownership");
            }

            var rq = new RecordShareUpdateCommand();
            var ro = new RecordShareObject
            {
                ToUsername = username,
                RecordUid = recordUid,
                Transfer = true,
            };

            var pkRq = new AuthProto.GetPublicKeysRequest();
            pkRq.Usernames.Add(username);

            var pkRss = await Auth.ExecuteAuthRest<AuthProto.GetPublicKeysRequest, AuthProto.GetPublicKeysResponse>("vault/get_public_keys", pkRq);
            var pkRs = pkRss.KeyResponses[0];
            if (pkRs.PublicKey.IsEmpty && pkRs.PublicEccKey.IsEmpty)
            {
                throw new KeeperApiException("public_key_error", pkRs.Message);
            }
            var useEcKey = !pkRs.PublicEccKey.IsEmpty && record?.Version != 2;
            if (useEcKey)
            {
                var pk = CryptoUtils.LoadPublicEcKey(pkRs.PublicEccKey.ToByteArray());
                ro.RecordKey = CryptoUtils.EncryptEc(record.RecordKey, pk).Base64UrlEncode();
                ro.useEccKey = true;
            }
            else
            {
                var pk = CryptoUtils.LoadPublicKey(pkRs.PublicKey.ToByteArray());
                ro.RecordKey = CryptoUtils.EncryptRsa(record.RecordKey, pk).Base64UrlEncode();
            }

            rq.AddShares = new[] { ro };
            var rs = await Auth.ExecuteAuthCommand<RecordShareUpdateCommand, Commands.RecordShareUpdateResponse>(rq);
            if (rs.AddStatuses != null)
            {
                var status = rs.AddStatuses.FirstOrDefault(x => string.Equals(x.RecordUid, recordUid) && string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase));
                if (status != null && !string.Equals(status.Status, "success", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new KeeperApiException(status.Status, status.Message);
                }
            }
        }

        /// <inheritdoc/>
        public async Task RevokeShareFromUser(string recordUid, string username)
        {
            if (!TryGetKeeperRecord(recordUid, out var record))
            {
                throw new KeeperApiException("not_found", "Record not found");
            }
            var rq = new RecordShareUpdateCommand();
            var ro = new RecordShareObject
            {
                ToUsername = username,
                RecordUid = recordUid,
            };
            rq.RemoveShares = new[] { ro };
            var rs = await Auth.ExecuteAuthCommand<RecordShareUpdateCommand, Commands.RecordShareUpdateResponse>(rq);
            if (rs.RemoveStatuses != null)
            {
                var status = rs.RemoveStatuses.FirstOrDefault(x => string.Equals(x.RecordUid, recordUid) && string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase));
                if (status != null && !string.Equals(status.Status, "success", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new KeeperApiException(status.Status, status.Message);
                }
            }
        }

        private readonly ISet<string> _recordsForAudit = new HashSet<string>();

        internal void ScheduleForAudit(params string[] recordUids)
        {
            if (Auth?.AuthContext?.EnterprisePublicEcKey != null)
            {
                lock (_recordsForAudit)
                {
                    _recordsForAudit.UnionWith(recordUids);
                }
            }
        }

        internal override void OnDataRebuilt()
        {
            base.OnDataRebuilt();

            string[] recordUids = null;
            lock (_recordsForAudit)
            {
                if (_recordsForAudit.Count > 0)
                {
                    recordUids = _recordsForAudit.ToArray();
                    _recordsForAudit.Clear();
                }
            }

            if (recordUids == null || recordUids.Length == 0) return;
            var publicEcKey = Auth?.AuthContext?.EnterprisePublicEcKey;
            if (publicEcKey == null) return;

            _ = Task.Run(async () =>
            {
                var auditData = recordUids
                    .Select(x => TryGetKeeperRecord(x, out var r) ? r : null)
                    .OfType<PasswordRecord>()
                    .Select(x =>
                    {
                        var rad = x.ExtractRecordAuditData();
                        return new Records.RecordAddAuditData
                        {
                            RecordUid = ByteString.CopyFrom(x.Uid.Base64UrlDecode()),
                            Revision = x.Revision,
                            Data = ByteString.CopyFrom(CryptoUtils.EncryptEc(JsonUtils.DumpJson(rad), publicEcKey))
                        };

                    })
                    .ToList();

                try
                {
                    while (auditData.Count > 0)
                    {
                        var rq = new AddAuditDataRequest();
                        rq.Records.AddRange(auditData.Take(999));
                        if (auditData.Count > 999)
                        {
                            auditData.RemoveRange(0, 999);
                        }
                        else
                        {
                            auditData.Clear();
                        }

                        await Auth.ExecuteAuthRest("vault/record_add_audit_data", rq);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

            });
        }
        /// <inheritdoc/>
        public IEnumerable<IAttachment> RecordAttachments(KeeperRecord record)
        {
            switch (record)
            {
                case PasswordRecord password:
                    if (password.Attachments != null)
                    {
                        foreach (var atta in password.Attachments)
                        {
                            yield return atta;
                        }
                    }

                    break;

                case TypedRecord typed:
                    var fileRef = typed.Fields
                        .Where(x => x.FieldName == "fileRef")
                        .OfType<TypedField<string>>().FirstOrDefault();
                    if (fileRef != null)
                    {
                        foreach (var fileUid in fileRef.Values)
                        {
                            if (TryGetKeeperRecord(fileUid, out var kr))
                            {
                                if (kr is FileRecord fr)
                                {
                                    yield return fr;
                                }
                            }
                        }
                    }

                    break;

                case FileRecord file:
                    yield return file;
                    break;
            }
        }


        /// <inheritdoc/>
        public async Task DownloadAttachment(KeeperRecord record, string attachment, Stream destination)
        {
            var atta = RecordAttachments(record)
                .Where(x =>
                {
                    if (string.IsNullOrEmpty(attachment))
                    {
                        return true;
                    }

                    if (attachment == x.Id || attachment == x.Name || attachment == x.Title)
                    {
                        return true;
                    }

                    return false;

                })
                .FirstOrDefault();

            if (atta == null)
            {
                throw new KeeperInvalidParameter("Vault::DownloadAttachment", "attachment", attachment, "not found");
            }

            switch (atta)
            {
                case AttachmentFile attachmentFile:
                    await DownloadAttachmentFile(record.Uid, attachmentFile, destination);
                    break;

                case FileRecord fileRecord:
                    await DownloadFile(fileRecord, destination);
                    break;

                default:
                    throw new KeeperInvalidParameter("Vault::DownloadAttachment", "attachment", atta.GetType().Name, "attachment type is not supported");

            }
        }


        /// <inheritdoc/>
        public async Task UploadAttachment(KeeperRecord record, IAttachmentUploadTask uploadTask)
        {
            switch (record)
            {
                case PasswordRecord password:
                    await UploadPasswordAttachment(password, uploadTask);
                    break;

                case TypedRecord typed:
                    await UploadTypedAttachment(typed, uploadTask);
                    break;
                default:
                    throw new KeeperInvalidParameter("Vault::UploadAttachment", "record", record.GetType().Name, "unsupported record type");
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAttachment(KeeperRecord record, string attachmentId)
        {
            var deleted = false;
            switch (record)
            {
                case PasswordRecord password:
                    if (password.Attachments != null)
                    {
                        var atta = password.Attachments.FirstOrDefault(x => x.Id == attachmentId);
                        if (atta != null)
                        {
                            deleted = password.Attachments.Remove(atta);
                        }
                    }

                    break;
                case TypedRecord typed:
                    var fileRef = typed.Fields
                        .Where(x => x.FieldName == "fileRef")
                        .OfType<TypedField<string>>().FirstOrDefault();
                    if (fileRef != null)
                    {
                        deleted = fileRef.Values.Remove(attachmentId);
                    }

                    break;
            }

            if (deleted)
            {
                await UpdateRecord(record, false);
            }

            return deleted;
        }



        /// <exclude/>
        public async Task DownloadFile(FileRecord fileRecord, Stream destination)
        {
            var rq = new Records.FilesGetRequest
            {
                ForThumbnails = false
            };
            rq.RecordUids.Add(ByteString.CopyFrom(fileRecord.Uid.Base64UrlDecode()));
            var rs = await Auth.ExecuteAuthRest<Records.FilesGetRequest, Records.FilesGetResponse>(
                "vault/files_download", rq);
            var fileResult = rs.Files[0];
            if (fileResult.Status != Records.FileGetResult.FgSuccess)
            {
                var status = fileResult.Status.ToString().ToSnakeCase();
                if (status.StartsWith("fg_"))
                {
                    status = status.Substring(3);
                }

                throw new KeeperApiException(status, fileRecord.Name ?? fileRecord.Title);
            }

            var request = WebRequest.Create(new Uri(fileResult.Url));

            using (var response = (HttpWebResponse) await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    var transform = new DecryptAesV2Transform(fileRecord.RecordKey);
                    using (var decodeStream = new CryptoStream(stream, transform, CryptoStreamMode.Read))
                    {
                        if (destination != null)
                        {
                            await decodeStream.CopyToAsync(destination);
                        }
                    }
                }
            }
        }

        /// <exclude />
        public async Task DownloadAttachmentFile(string recordUid, AttachmentFile attachment, Stream destination)
        {
            var command = new RequestDownloadCommand
            {
                RecordUid = recordUid,
                FileIDs = new[] { attachment.Id }
            };
            this.ResolveRecordAccessPath(command);
            var rs = await this.Auth.ExecuteAuthCommand<RequestDownloadCommand, RequestDownloadResponse>(command);

            var download = rs.Downloads[0];
            var request = WebRequest.Create(new Uri(download.Url));
            using (var response = (HttpWebResponse) await request.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            {
                var transform = new DecryptAesV1Transform(attachment.Key.Base64UrlDecode());
                using (var decodeStream = new CryptoStream(stream, transform, CryptoStreamMode.Read))
                {
                    if (destination != null)
                    {
                        await decodeStream.CopyToAsync(destination);
                    }
                }
            }
        }

        internal static async Task UploadSingleFile(UploadParameters upload, Stream source)
        {
            var boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            var boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary);

            var request = (HttpWebRequest) WebRequest.Create(new Uri(upload.Url));
            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=" + boundary;

            using (var requestStream = await Task.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, null))
            {
                const string parameterTemplate = "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                if (upload.Parameters != null)
                {
                    foreach (var pair in upload.Parameters)
                    {
                        await requestStream.WriteAsync(boundaryBytes, 0, boundaryBytes.Length);
                        var formItem = string.Format(parameterTemplate, pair.Key, pair.Value);
                        var formItemBytes = Encoding.UTF8.GetBytes(formItem);
                        await requestStream.WriteAsync(formItemBytes, 0, formItemBytes.Length);
                    }
                }

                await requestStream.WriteAsync(boundaryBytes, 0, boundaryBytes.Length);
                const string fileTemplate = "\r\nContent-Disposition: form-data; name=\"{0}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                var fileItem = string.Format(fileTemplate, upload.FileParameter);
                var fileBytes = Encoding.UTF8.GetBytes(fileItem);
                await requestStream.WriteAsync(fileBytes, 0, fileBytes.Length);

                await source.CopyToAsync(requestStream);

                await requestStream.WriteAsync(boundaryBytes, 0, boundaryBytes.Length);
                var trailer = Encoding.ASCII.GetBytes("--\r\n");
                await requestStream.WriteAsync(trailer, 0, trailer.Length);
            }

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse) await Task.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null);
                if ((int) response.StatusCode != upload.SuccessStatusCode)
                {
                    throw new KeeperInvalidParameter("Vault::UploadSingleFile", "StatusCode", response.StatusCode.ToString(), "not success");
                }
            }
            catch (WebException e)
            {
                response = (HttpWebResponse) e.Response;
                if (response == null || response.ContentType != "application/xml") throw;
                using (var stream = new MemoryStream())
                {
                    var srcStream = response.GetResponseStream();
                    if (srcStream == null) throw;
                    await srcStream.CopyToAsync(stream);
                    var responseText = Encoding.UTF8.GetString(stream.ToArray());
                    Trace.TraceError(responseText);
                }

                throw;
            }
        }

        /// <exclude />
        public async Task UploadPasswordAttachment(PasswordRecord record, IAttachmentUploadTask uploadTask)
        {
            var fileStream = uploadTask.Stream;
            if (fileStream == null)
            {
                throw new KeeperInvalidParameter("Vault::UploadAttachment", "uploadTask", "GetStream()", "null");
            }

            var thumbStream = uploadTask.Thumbnail?.Stream;
            var command = new RequestUploadCommand
            {
                FileCount = 1,
                ThumbnailCount = thumbStream != null ? 1 : 0
            };

            var rs = await Auth.ExecuteAuthCommand<RequestUploadCommand, RequestUploadResponse>(command);
            if (rs.FileUploads == null || rs.FileUploads.Length < 1)
            {
                throw new KeeperInvalidParameter("Vault::UploadAttachment", "request_upload", "file_uploads", "empty");
            }

            var fileUpload = rs.FileUploads[0];
            UploadParameters thumbUpload = null;
            if (rs.ThumbnailUploads != null && rs.ThumbnailUploads.Length > 0)
            {
                thumbUpload = rs.ThumbnailUploads[0];
            }

            var key = CryptoUtils.GenerateEncryptionKey();
            var atta = new AttachmentFile
            {
                Id = fileUpload.FileId,
                Name = uploadTask.Name,
                Title = uploadTask.Title,
                Key = key.Base64UrlEncode(),
                MimeType = uploadTask.MimeType,
                LastModified = DateTimeOffset.Now,
            };
            var transform = new EncryptAesV1Transform(key);
            using (var cryptoStream = new CryptoStream(fileStream, transform, CryptoStreamMode.Read))
            {
                await UploadSingleFile(fileUpload, cryptoStream);
                atta.Size = transform.EncryptedBytes;
            }

            if (thumbUpload != null && thumbStream != null)
            {
                try
                {
                    transform = new EncryptAesV1Transform(key);
                    using (var cryptoStream = new CryptoStream(thumbStream, transform, CryptoStreamMode.Read))
                    {
                        await UploadSingleFile(thumbUpload, cryptoStream);
                    }

                    var thumbnail = new AttachmentFileThumb
                    {
                        Id = thumbUpload.FileId,
                        Type = uploadTask.Thumbnail.MimeType,
                        Size = uploadTask.Thumbnail.Size
                    };
                    var ts = new[] { thumbnail };
                    atta.Thumbnails = atta.Thumbnails == null ? ts : atta.Thumbnails.Concat(ts).ToArray();
                }
                catch (Exception e)
                {
                    Trace.TraceError("Upload Thumbnail: {0}: \"{1}\"", e.GetType().Name, e.Message);
                }
            }

            record.Attachments.Add(atta);

            await UpdateRecord(record);
        }


        /// <exclude />
        public async Task UploadTypedAttachment(TypedRecord record, IAttachmentUploadTask uploadTask)
        {
            var fileStream = uploadTask.Stream;
            if (fileStream == null)
            {
                throw new KeeperInvalidParameter("Vault::UploadAttachment", "uploadTask", "GetStream()", "null");
            }

            var fileData = new RecordFileData
            {
                Type = uploadTask.MimeType,
                Name = uploadTask.Name,
                Title = uploadTask.Title,
                Size = null,
                ThumbnailSize = null,
                LastModified = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),

            };
            var fileKey = CryptoUtils.GenerateEncryptionKey();
            byte[] encryptedThumb = null;
            if (uploadTask.Thumbnail != null)
            {
                using (var ts = new MemoryStream())
                {
                    await uploadTask.Stream.CopyToAsync(ts);
                    await ts.FlushAsync();
                    var thumbBytes = ts.ToArray();
                    fileData.ThumbnailSize = thumbBytes.Length;
                    encryptedThumb = CryptoUtils.EncryptAesV2(thumbBytes, fileKey);
                }
            }

            var tempFile = Path.GetTempFileName();
            var transform = new EncryptAesV2Transform(fileKey);
            using (var encryptedFile = System.IO.File.OpenWrite(tempFile))
            using (var cryptoStream = new CryptoStream(uploadTask.Stream, transform, CryptoStreamMode.Read))
            {
                await cryptoStream.CopyToAsync(encryptedFile);
                fileData.Size = transform.EncryptedBytes;
            }

            var fileInfo = new FileInfo(tempFile);
            var fileUid = CryptoUtils.GenerateUid();
            var fileRq = new Records.File
            {
                RecordUid = ByteString.CopyFrom(fileUid.Base64UrlDecode()),
                RecordKey = ByteString.CopyFrom(CryptoUtils.EncryptAesV2(fileKey, Auth.AuthContext.DataKey)),
                Data = ByteString.CopyFrom(CryptoUtils.EncryptAesV2(JsonUtils.DumpJson(fileData), fileKey)),
                FileSize = fileInfo.Length,
                ThumbSize = encryptedThumb?.Length ?? 0,
            };
            var rq = new Records.FilesAddRequest
            {
                ClientTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
            rq.Files.Add(fileRq);
            var fileRs = await Auth.ExecuteAuthRest<Records.FilesAddRequest, Records.FilesAddResponse>("vault/files_add", rq);
            var uploadRs = fileRs.Files[0];
            var fileUpload = new UploadParameters
            {
                Url = uploadRs.Url,
                FileParameter = "file",
                SuccessStatusCode = uploadRs.SuccessStatusCode,
                Parameters = JsonUtils.ParseJson<Dictionary<string, object>>(Encoding.UTF8.GetBytes(uploadRs.Parameters))
            };

            try
            {
                using (var cryptoStream = System.IO.File.OpenRead(tempFile))
                {
                    await UploadSingleFile(fileUpload, cryptoStream);
                }
            }
            catch (Exception e)
            {
                Trace.TraceError("Upload Thumbnail: {0}: \"{1}\"", e.GetType().Name, e.Message);
            }

            if (encryptedThumb != null && !string.IsNullOrEmpty(uploadRs.ThumbnailParameters))
            {
                var thumbUpload = new UploadParameters
                {
                    Url = uploadRs.Url,
                    FileParameter = "thumb",
                    SuccessStatusCode = uploadRs.SuccessStatusCode,
                    Parameters = JsonUtils.ParseJson<Dictionary<string, object>>(Encoding.UTF8.GetBytes(uploadRs.ThumbnailParameters))
                };
                try
                {
                    using (var cryptoStream = new MemoryStream(encryptedThumb))
                    {
                        await UploadSingleFile(thumbUpload, cryptoStream);
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError("Upload Thumbnail: {0}: \"{1}\"", e.GetType().Name, e.Message);
                }
            }

            var facade = new TypedRecordFacade<TypedRecordFileRef>(record);
            if (facade.Fields.FileRef != null)
            {
                var uids = facade.Fields.FileRef.Values;
                if (uids.Count > 0 && string.IsNullOrEmpty(uids[0]))
                {
                    uids[0] = fileUid;
                }
                else
                {
                    uids.Add(fileUid);
                }
            }

            await UpdateRecord(record);
        }
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
        /// <inheritdoc/>>
        public async Task PutUserToSharedFolder(string sharedFolderUid,
            string userId,
            UserType userType,
            ISharedFolderUserOptions options)
        {
            var sharedFolder = this.GetSharedFolder(sharedFolderUid);
            var perm = this.ResolveSharedFolderAccessPath(Auth.Username, sharedFolderUid, true);
            if (perm == null)
            {
                throw new VaultException("You don't have permission to manage users.");
            }

            var request = new SharedFolderUpdateCommand
            {
                pt = Auth.AuthContext.SessionToken.Base64UrlEncode(),
                operation = "update",
                shared_folder_uid = sharedFolder.Uid,
                from_team_uid = perm.UserType == UserType.Team ? perm.UserId : null,
                name = CryptoUtils.EncryptAesV1(Encoding.UTF8.GetBytes(sharedFolder.Name), sharedFolder.SharedFolderKey).Base64UrlEncode(),
                forceUpdate = true,
            };
            if (userType == UserType.User)
            {
                if (sharedFolder.UsersPermissions.Any(x => x.UserType == UserType.User && x.UserId == userId))
                {
                    request.updateUsers = new[]
                    {
                        new SharedFolderUpdateUser
                        {
                            Username = userId,
                            ManageUsers = options?.ManageUsers,
                            ManageRecords = options?.ManageRecords,
                        }
                    };
                }
                else
                {
                    var keyTuple = await GetUserPublicKeys(userId);
                    var publicKey = CryptoUtils.LoadPublicKey(keyTuple.Item1);
                    request.addUsers = new[]
                    {
                        new SharedFolderUpdateUser
                        {
                            Username = userId,
                            ManageUsers = options?.ManageUsers,
                            ManageRecords = options?.ManageRecords,
                            SharedFolderKey = CryptoUtils.EncryptRsa(sharedFolder.SharedFolderKey, publicKey).Base64UrlEncode(),
                        }
                    };
                }
            }
            else
            {
                if (sharedFolder.UsersPermissions.Any(x => x.UserType == UserType.Team && x.UserId == userId))
                {
                    request.updateTeams = new[]
                    {
                        new SharedFolderUpdateTeam
                        {
                            TeamUid = userId,
                            ManageUsers = options?.ManageUsers,
                            ManageRecords = options?.ManageRecords,
                        }
                    };
                }
                else
                {
                    string encryptedSharedFolderKey;
                    if (TryGetTeam(userId, out var team))
                    {
                        encryptedSharedFolderKey = CryptoUtils.EncryptAesV1(sharedFolder.SharedFolderKey, team.TeamKey).Base64UrlEncode();
                    }
                    else
                    {
                        var tkRq = new TeamGetKeysCommand
                        {
                            teams = new[] { userId },
                        };
                        var tkRs = await Auth.ExecuteAuthCommand<TeamGetKeysCommand, TeamGetKeysResponse>(tkRq);
                        if (tkRs.keys == null || tkRs.keys.Length == 0)
                        {
                            throw new VaultException($"Cannot get public key of team: {userId}");
                        }

                        var tk = tkRs.keys[0];
                        if (!string.IsNullOrEmpty(tk.resultCode))
                        {
                            throw new KeeperApiException(tk.resultCode, tk.message);
                        }

                        var tpk = CryptoUtils.LoadPublicKey(tk.key.Base64UrlDecode());
                        encryptedSharedFolderKey = CryptoUtils.EncryptRsa(sharedFolder.SharedFolderKey, tpk).Base64UrlEncode();
                    }

                    request.addTeams = new[]
                    {
                        new SharedFolderUpdateTeam
                        {
                            TeamUid = userId,
                            ManageUsers = options?.ManageUsers,
                            ManageRecords = options?.ManageRecords,
                            SharedFolderKey = encryptedSharedFolderKey,
                        }
                    };
                }
            }


            var response = await Auth.ExecuteAuthCommand<SharedFolderUpdateCommand, SharedFolderUpdateResponse>(request);
            foreach (var arr in (new[] { response.addUsers, response.updateUsers }))
            {
                var failed = arr?.FirstOrDefault(x => x.Status != "success");
                if (failed != null)
                {
                    throw new VaultException($"Put \"{failed.Username}\" to Shared Folder \"{sharedFolder.Name}\" error: {failed.Status}");
                }
            }

            foreach (var arr in (new[] { response.addTeams, response.updateTeams }))
            {
                var failed = arr?.FirstOrDefault(x => x.Status != "success");
                if (failed != null)
                {
                    throw new VaultException($"Put Team Uid \"{failed.TeamUid}\" to Shared Folder \"{sharedFolder.Name}\" error: {failed.Status}");
                }
            }

            await ScheduleSyncDown(TimeSpan.FromSeconds(0));
        }

        /// <inheritdoc/>>
        public async Task RemoveUserFromSharedFolder(string sharedFolderUid, string userId, UserType userType)
        {
            var sharedFolder = this.GetSharedFolder(sharedFolderUid);
            var perm = this.ResolveSharedFolderAccessPath(Auth.Username, sharedFolderUid, true);
            if (perm == null)
            {
                throw new VaultException("You don't have permission to manage teams.");
            }

            if (!sharedFolder.UsersPermissions.Any(x => x.UserType == userType
                && string.Compare(x.UserId, userId, StringComparison.InvariantCultureIgnoreCase) == 0))
            {
                return;
            }

            var request = new SharedFolderUpdateCommand
            {
                pt = Auth.AuthContext.SessionToken.Base64UrlEncode(),
                operation = "update",
                shared_folder_uid = sharedFolder.Uid,
                from_team_uid = perm.UserType == UserType.Team ? perm.UserId : null,
                name = CryptoUtils.EncryptAesV1(Encoding.UTF8.GetBytes(sharedFolder.Name), sharedFolder.SharedFolderKey).Base64UrlEncode(),
                forceUpdate = true,
            };
            if (userType == UserType.User)
            {
                request.removeUsers = new[] { new SharedFolderUpdateUser { Username = userId } };
            }
            else
            {
                request.removeTeams = new[] { new SharedFolderUpdateTeam { TeamUid = userId } };
            }

            var response = await Auth.ExecuteAuthCommand<SharedFolderUpdateCommand, SharedFolderUpdateResponse>(request);
            foreach (var arr in (new[] { response.removeUsers }))
            {
                var failed = arr?.FirstOrDefault(x => x.Status != "success");
                if (failed != null)
                {
                    throw new VaultException($"Remove User \"{failed.Username}\" from Shared Folder \"{sharedFolder.Name}\" error: {failed.Status}");
                }
            }

            foreach (var arr in (new[] { response.removeTeams }))
            {
                var failed = arr?.FirstOrDefault(x => x.Status != "success");
                if (failed != null)
                {
                    throw new VaultException($"Remove Team \"{failed.TeamUid}\" from Shared Folder \"{sharedFolder.Name}\" error: {failed.Status}");
                }
            }

            await ScheduleSyncDown(TimeSpan.FromSeconds(0));
        }

        /// <inheritdoc/>>
        public async Task ChangeRecordInSharedFolder(string sharedFolderUid, string recordUid, ISharedFolderRecordOptions options)
        {
            var sharedFolder = this.GetSharedFolder(sharedFolderUid);
            var perm = this.ResolveSharedFolderAccessPath(Auth.Username, sharedFolderUid, false, true);
            if (perm == null)
            {
                throw new VaultException("You don't have permission to manage records.");
            }

            _ = this.GetRecord(recordUid);
            var recordPerm = sharedFolder.RecordPermissions.FirstOrDefault(x => x.RecordUid != recordUid);
            if (recordPerm != null && options != null)
            {
                var sfur = new SharedFolderUpdateRecord
                {
                    RecordUid = recordUid,
                    CanEdit = options.CanEdit ?? recordPerm.CanEdit,
                    CanShare = options.CanShare ?? recordPerm.CanShare,
                };
                var recordPath = this.ResolveRecordAccessPath(sfur, options.CanEdit.HasValue, options.CanShare.HasValue);
                if (recordPath == null)
                {
                    throw new VaultException($"You don't have permission to edit and/or share the record UID \"{recordUid}\"");
                }

                var request = new SharedFolderUpdateCommand
                {
                    pt = Auth.AuthContext.SessionToken.Base64UrlEncode(),
                    operation = "update",
                    shared_folder_uid = sharedFolder.Uid,
                    from_team_uid = perm.UserType == UserType.Team ? perm.UserId : null,
                    name = CryptoUtils.EncryptAesV1(Encoding.UTF8.GetBytes(sharedFolder.Name), sharedFolder.SharedFolderKey).Base64UrlEncode(),
                    forceUpdate = true,
                    updateRecords = new[] { sfur }
                };

                var response = await Auth.ExecuteAuthCommand<SharedFolderUpdateCommand, SharedFolderUpdateResponse>(request);
                foreach (var arr in (new[] { response.updateRecords }))
                {
                    var failed = arr?.FirstOrDefault(x => x.Status != "success");
                    if (failed != null)
                    {
                        throw new VaultException($"Put Record UID \"{failed.RecordUid}\" to Shared Folder \"{sharedFolder.Name}\" error: {failed.Status}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Record UID ({recordUid}) cannot be found in Shared Folder ({sharedFolder.Name})");
            }

            await ScheduleSyncDown(TimeSpan.FromSeconds(0));
        }
        /*
        public async Task RemoveRecordFromSharedFolder(string sharedFolderUid, string recordUid)
        {
            var sharedFolder = this.GetSharedFolder(sharedFolderUid);
            var perm = this.ResolveSharedFolderAccessPath(Auth.Username, sharedFolderUid, false, true);
            if (perm == null)
            {
                throw new VaultException("You don't have permission to manage records.");
            }

            if (sharedFolder.RecordPermissions.All(x => x.RecordUid != recordUid))
            {
                return;
            }

            var request = new SharedFolderUpdateCommand
            {
                pt = Auth.AuthContext.SessionToken.Base64UrlEncode(),
                operation = "update",
                shared_folder_uid = sharedFolder.Uid,
                from_team_uid = perm.UserType == UserType.Team ? perm.UserId : null,
                name = CryptoUtils.EncryptAesV1(Encoding.UTF8.GetBytes(sharedFolder.Name), sharedFolder.SharedFolderKey).Base64UrlEncode(),
                forceUpdate = true,
                removeRecords = new[] {new SharedFolderUpdateRecord {RecordUid = recordUid}}
            };
            var response = await Auth.ExecuteAuthCommand<SharedFolderUpdateCommand, SharedFolderUpdateResponse>(request);
            foreach (var arr in (new[] {response.removeRecords}))
            {
                var failed = arr?.FirstOrDefault(x => x.Status != "success");
                if (failed != null)
                {
                    throw new VaultException($"Remove Record Uid \"{failed.RecordUid}\" to Shared Folder \"{sharedFolder.Name}\" error: {failed.Status}");
                }
            }

            await ScheduleSyncDown(TimeSpan.FromSeconds(0));
        }
        */
    }
}