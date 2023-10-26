using Authentication;
using Enterprise;
using Google.Protobuf;
using KeeperSecurity.Authentication;
using KeeperSecurity.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace KeeperSecurity.Utils
{
    /// <exclude />
    public static class ManageAccountExtension
    {
        internal static async Task ShareAccount(this IAuthentication auth, AccountShareTo[] shareAccountTo)
        {
            if (shareAccountTo != null)
                foreach (var shareTo in shareAccountTo)
                {
                    var key = CryptoUtils.LoadPublicKey(shareTo.PublicKey.Base64UrlDecode());
                    var command = new ShareAccountCommand
                    {
                        ToRoleId = shareTo.RoleId,
                        TransferKey = CryptoUtils.EncryptRsa(auth.AuthContext.DataKey, key).Base64UrlEncode()
                    };
                    await auth.ExecuteAuthCommand(command);
                }
        }

        public static async Task<NewUserMinimumParams> GetNewUserParams(this IKeeperEndpoint endpoint, string username)
        {
            var authRequest = new DomainPasswordRulesRequest
            {
                Username = username
            };
            var payload = new ApiRequestPayload
            {
                Payload = ByteString.CopyFrom(authRequest.ToByteArray())
            };
            var rs = await endpoint.ExecuteRest("authentication/get_domain_password_rules", payload);
            return NewUserMinimumParams.Parser.ParseFrom(rs);
        }

        public static async Task<string> ChangeMasterPassword(this IAuthentication auth)
        {
            if (auth.AuthCallback is IPostLoginTaskUI postUi)
            {
                var userParams = await auth.Endpoint.GetNewUserParams(auth.Username);

                var rules = userParams.PasswordMatchDescription
                    .Zip(userParams.PasswordMatchRegex,
                        (description, pattern) => new PasswordRule
                        {
                            description = description,
                            match = true,
                            pattern = pattern
                        })
                    .ToArray();
                var ruleMatcher = new PasswordRuleMatcher(rules);

                var newPassword = await postUi.GetNewPassword(ruleMatcher);

                var failedRules = ruleMatcher.MatchFailedRules(newPassword);
                if (failedRules.Length != 0) throw new KeeperApiException("password_rule_failed", failedRules[0]);

                var iterations = 100000;
                var authSalt = CryptoUtils.GetRandomBytes(16);
                var authVerifier = CryptoUtils.CreateAuthVerifier(newPassword, authSalt, iterations);
                var keySalt = CryptoUtils.GetRandomBytes(16);
                var encryptionParameters = CryptoUtils.CreateEncryptionParams(newPassword, keySalt, iterations, auth.AuthContext.DataKey);

                var command = new ChangeMasterPasswordCommand
                {
                    AuthVerifier = authVerifier.Base64UrlEncode(),
                    EncryptionParams = encryptionParameters.Base64UrlEncode()
                };

                await auth.ExecuteAuthCommand(command);
                return newPassword;
            }

            return null;
        }
    }
}
