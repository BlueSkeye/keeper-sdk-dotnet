using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Authentication;
using Google.Protobuf;
using KeeperSecurity.Commands;
using KeeperSecurity.Utils;
using PasswordRule = KeeperSecurity.Commands.PasswordRule;

namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Represents base authentication class
    /// </summary>
    /// <seealso cref="Auth"/>
    /// <seealso cref="AuthSync"/>
    public abstract class AuthCommon : IAuthentication, IDisposable
    {
        /// <inheritdoc/>
        public IKeeperEndpoint Endpoint { get; protected set; }

        /// <inheritdoc/>
        public string Username { get; protected set; }

        /// <inheritdoc/>
        public byte[] DeviceToken { get; protected set; }

        internal AuthContext authContext;

        /// <inheritdoc/>
        public IAuthContext AuthContext => authContext;

        /// <exclude/>
        public IFanOut<NotificationEvent> PushNotifications { get; internal set; }

        /// <exclude/>
        public abstract IAuthCallback AuthCallback { get; }

        internal void ResetKeepAliveTimer()
        {
            _lastRequestTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000;
        }

        private Timer _timer;
        private long _lastRequestTime;

        internal void SetKeepAliveTimer(int timeoutInMinutes, IAuthentication auth)
        {
            _timer?.Dispose();
            _timer = null;
            if (auth == null) return;

            ResetKeepAliveTimer();
            var timeout = TimeSpan.FromMinutes(timeoutInMinutes - (timeoutInMinutes > 1 ? 1 : 0));
            _timer = new Timer(async (_) =>
                {
                    var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000;
                    if (_lastRequestTime + timeout.TotalSeconds / 2 > now) return;
                    try
                    {
                        await auth.ExecuteAuthRest("keep_alive", null);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                        _timer.Dispose();
                        _timer = null;
                    }

                    _lastRequestTime = now;
                },
                null,
                (long) timeout.TotalMilliseconds / 2,
                (long) timeout.TotalMilliseconds / 2);
        }

        /// <inheritdoc/>
        public async Task<KeeperApiResponse> ExecuteAuthCommand(AuthenticatedCommand command, Type responseType, bool throwOnError)
        {
            command.username = Username;
            command.sessionToken = authContext.SessionToken.Base64UrlEncode();
            var response = await Endpoint.ExecuteV2Command(command, responseType);
            if (response.IsSuccess)
            {
                ResetKeepAliveTimer();
                return response;
            }

            if (response.resultCode == "auth_failed")
            {
                throw new KeeperAuthFailed(response.message);
            }

            if (throwOnError)
            {
                throw new KeeperApiException(response.resultCode, response.message);
            }

            return response;
        }

        /// <inheritdoc/>
        public async Task<IMessage> ExecuteAuthRest(string endpoint, IMessage request, Type responseType = null)
        {
#if DEBUG
            Debug.WriteLine($"REST Request: endpoint \"{endpoint}\": {request}");
#endif
            var rq = new ApiRequestPayload
            {
                EncryptedSessionToken = ByteString.CopyFrom(authContext.SessionToken),
                ApiVersion = 3,
            };
            if (request != null)
            {
                rq.Payload = request.ToByteString();
            }

            var rsBytes = await Endpoint.ExecuteRest(endpoint, rq);
            this.ResetKeepAliveTimer();
            if (responseType == null) return null;

            var responseParser = responseType.GetProperty("Parser", BindingFlags.Static | BindingFlags.Public);
            if (responseParser == null) throw new KeeperInvalidParameter("ExecuteAuthRest", "responseType", responseType.Name, "Google Protobuf class expected");
            var mp = (MessageParser) (responseParser.GetMethod.Invoke(null, null));

            var response = mp.ParseFrom(rsBytes);
#if DEBUG
            Debug.WriteLine($"REST response: endpoint \"{endpoint}\": {response}");
#endif
            return response;
        }

        private bool _storeProxyReturned;
        protected virtual IWebProxy GetStoredProxy(Uri proxyUri, string[] proxyAuth)
        {
            if (_storeProxyReturned) return null;
            _storeProxyReturned = true;
#if NET452_OR_GREATER
            if (CredentialManager.GetCredentials(proxyUri.DnsSafeHost, out var username, out var password))
            {
                return AuthUIExtensions.GetWebProxyForCredentials(proxyUri, proxyAuth, username, password);
            }
#endif
            return null;
        }

        protected async Task<T> DetectProxy<T>(Uri uri, Func<Uri, string[], T> onProxyDetected)
            where T : class
        {
            IWebProxy proxy = Endpoint.WebProxy;
            do
            {
                try
                {
                    await PingKeeperServer(uri, proxy);
                    if (proxy != null)
                    {
                        Endpoint.WebProxy = proxy;
                    }

                    return null;
                }
                catch (WebException e)
                {
                    var response = (HttpWebResponse) e.Response;
                    if (response?.StatusCode != HttpStatusCode.ProxyAuthenticationRequired) return null;

                    var authHeader = response.Headers.AllKeys
                        .FirstOrDefault(x =>
                            string.Compare(x, "Proxy-Authenticate", StringComparison.OrdinalIgnoreCase) ==
                            0);
                    var systemProxy = WebRequest.GetSystemWebProxy();
                    var directUri = systemProxy.GetProxy(uri);
                    var proxyAuthenticate = KeeperSettings.ParseProxyAuthentication(authHeader).ToArray();


                    var storedProxy = GetStoredProxy(directUri, proxyAuthenticate);
                    if (storedProxy != null && ReferenceEquals(proxy, storedProxy))
                    {
                        storedProxy = null;
                    }

                    proxy = storedProxy;
                    if (proxy != null) continue;

                    return onProxyDetected?.Invoke(directUri, proxyAuthenticate);
                }
            } while (true);
        }

        public virtual async Task<bool> PingKeeperServer(Uri serverUri, IWebProxy proxy)
        {
            var request = (HttpWebRequest) WebRequest.Create(serverUri);
            if (proxy != null)
            {
                request.Proxy = proxy;
            }
            using (var response = (HttpWebResponse) await request.GetResponseAsync())
            {
                if (response.StatusCode != HttpStatusCode.OK) return false;
                var rs = response.GetResponseStream();
                if (rs == null) return false;
                using (var sr = new StreamReader(rs, Encoding.UTF8))
                {
                    var status = await sr.ReadLineAsync();
                    return status == "alive";
                }
            }
        }

        /// <exclude/>
        public bool SupportRestrictedSession { get; set; }

        protected async Task PostLogin()
        {
            string clientKey = null;
            var accountSummaryResponse = await this.LoadAccountSummary();
            var license = AccountLicense.LoadFromProtobuf(accountSummaryResponse.License);
            var settings = AccountSettings.LoadFromProtobuf(accountSummaryResponse.Settings);
            var keys = AccountKeys.LoadFromProtobuf(accountSummaryResponse.KeysInfo);

            if (accountSummaryResponse.ClientKey?.Length > 0)
            {
                clientKey = accountSummaryResponse.ClientKey.ToByteArray().Base64UrlEncode();
            }

            IDictionary<string, object> enforcements = new Dictionary<string, object>();
            if (accountSummaryResponse.Enforcements?.Booleans != null)
            {
                foreach (var kvp in accountSummaryResponse.Enforcements.Booleans)
                {
                    enforcements[kvp.Key] = kvp.Value;
                }
            }

            if (accountSummaryResponse.Enforcements?.Strings != null)
            {
                foreach (var kvp in accountSummaryResponse.Enforcements.Strings)
                {
                    enforcements[kvp.Key] = kvp.Value;
                }
            }

            if (accountSummaryResponse.Enforcements?.Longs != null)
            {
                foreach (var kvp in accountSummaryResponse.Enforcements.Longs)
                {
                    enforcements[kvp.Key] = kvp.Value;
                }
            }

            if (accountSummaryResponse.Enforcements?.Jsons != null)
            {
                foreach (var kvp in accountSummaryResponse.Enforcements.Jsons)
                {
                    try
                    {
                        switch (kvp.Key)
                        {
                            case "password_rules":
                                var rules = JsonUtils.ParseJson<PasswordRule[]>(Encoding.UTF8.GetBytes(kvp.Value));
                                enforcements[kvp.Key] = rules;
                                break;
                            case "master_password_reentry":
                                var mpr = JsonUtils.ParseJson<MasterPasswordReentry>(Encoding.UTF8.GetBytes(kvp.Value));
                                enforcements[kvp.Key] = mpr;
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }

            var isEnterpriseAdmin = accountSummaryResponse.IsEnterpriseAdmin;
            if (keys.EncryptedPrivateKey != null)
            {
                var privateKeyData =
                    CryptoUtils.DecryptAesV1(keys.EncryptedPrivateKey.Base64UrlDecode(),
                        authContext.DataKey);
                authContext.PrivateRsaKey = CryptoUtils.LoadPrivateKey(privateKeyData);
            }
            if (keys.EncryptedEcPrivateKey != null) {
                var privateKeyData =
                    CryptoUtils.DecryptAesV2(keys.EncryptedEcPrivateKey.Base64UrlDecode(),
                        authContext.DataKey);
                authContext.PrivateEcKey = CryptoUtils.LoadPrivateEcKey(privateKeyData);
            }

            if (!string.IsNullOrEmpty(clientKey))
            {
                authContext.ClientKey = CryptoUtils.DecryptAesV1(clientKey.Base64UrlDecode(), authContext.DataKey);
            }

            authContext.License = license;
            authContext.Settings = settings;
            authContext.Enforcements = enforcements;
            authContext.IsEnterpriseAdmin = isEnterpriseAdmin;

            if (authContext.SessionTokenRestriction != 0)
            {
                if (AuthCallback is IPostLoginTaskUI postUi)
                {
                    if ((authContext.SessionTokenRestriction & SessionTokenRestriction.AccountExpired) != 0)
                    {
                        var accountExpiredDescription =
                            "Your Keeper account has expired. Please open the Keeper app to renew " +
                            $"or visit the Web Vault at https://{Endpoint.Server}/vault";
                        await postUi.Confirmation(accountExpiredDescription);
                    }
                    else
                    {
                        if ((authContext.SessionTokenRestriction & SessionTokenRestriction.AccountRecovery) != 0)
                        {
                            const string passwordExpiredDescription =
                                "Your Master Password has expired, you are required to change it before you can login.";
                            if (await postUi.Confirmation(passwordExpiredDescription))
                            {
                                var newPassword = await this.ChangeMasterPassword();

                                var validatorSalt = CryptoUtils.GetRandomBytes(16);
                                authContext.PasswordValidator =
                                    CryptoUtils.CreateEncryptionParams(newPassword, validatorSalt, 100000,
                                        CryptoUtils.GetRandomBytes(32));

                                authContext.SessionTokenRestriction &= ~SessionTokenRestriction.AccountRecovery;
                            }
                        }

                        if ((authContext.SessionTokenRestriction & SessionTokenRestriction.ShareAccount) != 0)
                        {
                            //expired_account_transfer_description
                            const string accountTransferDescription =
                                "Your Keeper administrator has changed your account settings to enable the ability to transfer your vault records at a later date, " +
                                "in accordance with company operating procedures and or policies." +
                                "\nPlease acknowledge this change in account settings by clicking 'Accept' or contact your administrator to request an extension." +
                                "\nDo you accept Account Transfer policy?";
                            if (await postUi.Confirmation(accountTransferDescription))
                            {
                                await this.ShareAccount(settings?.ShareAccountTo);
                                authContext.SessionTokenRestriction &= ~SessionTokenRestriction.ShareAccount;
                            }
                        }
                    }
                }

                if (authContext.SessionTokenRestriction == 0)
                {
                    // ???? relogin
                }
                else
                {
                    if (!SupportRestrictedSession)
                    {
                        if ((authContext.SessionTokenRestriction & SessionTokenRestriction.AccountExpired) != 0)
                        {
                            if (authContext.License?.AccountType == 0 && authContext.License?.ProductTypeId == 1)
                            {
                                throw new KeeperPostLoginErrors("free_trial_expired_please_purchase",
                                    "Your free trial has expired. Please purchase a subscription.");
                            }

                            throw new KeeperPostLoginErrors("expired_please_purchase",
                                "Your subscription has expired. Please purchase a subscription now.");
                        }

                        if ((authContext.SessionTokenRestriction & SessionTokenRestriction.AccountRecovery) != 0)
                        {
                            throw new KeeperPostLoginErrors("expired_master_password_description",
                                "Your Master Password has expired, you are required to change it before you can login.");
                        }

                        throw new KeeperPostLoginErrors("need_vault_settings_update",
                            "Please log into the web Vault to update your account settings.");
                    }
                }
            }
            else
            {
                if (authContext.Settings.LogoutTimerInSec.HasValue)
                {
                    if (authContext.Settings.LogoutTimerInSec > TimeSpan.FromMinutes(10).TotalSeconds &&
                        authContext.Settings.LogoutTimerInSec < TimeSpan.FromHours(12).TotalSeconds)
                    {
                        SetKeepAliveTimer(
                            (int) TimeSpan.FromSeconds(authContext.Settings.LogoutTimerInSec.Value).TotalMinutes, this);
                    }
                }

                if (authContext.License.AccountType == 2)
                {
                    try
                    {
                        var rs = (BreachWatch.EnterprisePublicKeyResponse) await ExecuteAuthRest(
                            "enterprise/get_enterprise_public_key", null,
                            typeof(BreachWatch.EnterprisePublicKeyResponse));
                        authContext.EnterprisePublicEcKey =
                            CryptoUtils.LoadPublicEcKey(rs.EnterpriseECCPublicKey.ToByteArray());
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }
        }

        /// <exclude/>
        public async Task AuditEventLogging(string eventType, AuditEventInput input = null)
        {
            if (AuthContext.EnterprisePublicEcKey != null)
            {
                var rq = new AuditEventLoggingCommand
                {
                    ItemLogs = new[] {
                        new AuditEventItem
                        {
                            AuditEventType = eventType,
                            Inputs = input
                        }
                    }
                };
                _ = await AuthExtensions.ExecuteAuthCommand<AuditEventLoggingCommand, AuditEventLoggingResponse>(this, rq);
            }
        }

        private async Task DoLogout()
        {
            try
            {
                if (this.IsAuthenticated())
                {
                    await ExecuteAuthRest("vault/logout_v3", null);
                    this.SsoLogout();
                }
            }
            finally
            {
                authContext = null;
                _timer?.Dispose();
                _timer = null;
            }
        }

        /// <inheritdoc/>
        public virtual async Task Logout()
        {
            await DoLogout();
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            authContext = null;
            PushNotifications?.Dispose();
            _timer?.Dispose();
        }
    }
}
