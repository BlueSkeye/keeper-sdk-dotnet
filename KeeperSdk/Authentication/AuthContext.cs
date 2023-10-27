using AccountSummary;
using KeeperSecurity.Utils;
using Org.BouncyCastle.Crypto.Parameters;
using System.Collections.Generic;

namespace KeeperSecurity.Authentication
{
    internal class AuthContext : IAuthContext
    {
        public byte[] DataKey { get; internal set; }
        public byte[] ClientKey { get; internal set; }
        public RsaPrivateCrtKeyParameters PrivateRsaKey { get; internal set; }
        public ECPrivateKeyParameters PrivateEcKey { get; internal set; }
        public ECPublicKeyParameters EnterprisePublicEcKey { get; internal set; }
        public byte[] SessionToken { get; internal set; }
        public SessionTokenRestriction SessionTokenRestriction { get; set; }
        public AccountLicense License { get; internal set; }
        public AccountSettings Settings { get; internal set; }
        public DeviceInfo DeviceInfo { get; internal set; }
        public IDictionary<string, object> Enforcements { get; internal set; }
        public bool IsEnterpriseAdmin { get; internal set; }
        public AccountAuthType AccountAuthType { get; set; }
        public SsoLoginInfo SsoLoginInfo { get; internal set; }
        internal byte[] PasswordValidator { get; set; }
        public bool CheckPasswordValid(string password)
        {
            if (PasswordValidator == null) return false;
            try
            {
                var rnd = CryptoUtils.DecryptEncryptionParams(password, PasswordValidator);
                return rnd?.Length == 32;
            }
            catch
            {
                return false;
            }
        }
        public RsaPrivateCrtKeyParameters PrivateKey => PrivateRsaKey;
    }
}
