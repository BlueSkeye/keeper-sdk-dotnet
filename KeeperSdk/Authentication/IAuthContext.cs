using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;

namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Defines properties of connected user.
    /// </summary>
    public interface IAuthContext
    {
        /// <summary>
        /// User's Data Key.
        /// </summary>
        byte[] DataKey { get; }

        /// <summary>
        /// Connection Token.
        /// </summary>
        byte[] SessionToken { get; }

        /// <summary>
        /// User's Client Key.
        /// </summary>
        byte[] ClientKey { get; }

        /// <summary>
        /// User's RSA Private Key.
        /// </summary>
        RsaPrivateCrtKeyParameters PrivateRsaKey { get; }

        /// <summary>
        /// User's EC Private key
        /// </summary>
        ECPrivateKeyParameters PrivateEcKey { get; }

        /// <summary>
        /// Enterprise EC Public key
        /// </summary>
        ECPublicKeyParameters EnterprisePublicEcKey { get; }

        /// <exclude/>
        [Obsolete("Use PrivateRsaKey")]
        RsaPrivateCrtKeyParameters PrivateKey { get; }

        /// <summary>
        /// Gets user's account license.
        /// </summary>
        AccountLicense License { get; }

        /// <summary>
        /// Gets user's account settings.
        /// </summary>
        AccountSettings Settings { get; }

        /// <summary>
        /// Gets user's enterprise enforcements.
        /// </summary>
        IDictionary<string, object> Enforcements { get; }

        /// <summary>
        /// Gets enterprise administrator flag.
        /// </summary>
        bool IsEnterpriseAdmin { get; }

        /// <summary>
        /// Gets account login type
        /// </summary>
        AccountAuthType AccountAuthType { get; }

        /// <summary>
        /// Gets SSO provider information
        /// </summary>
        SsoLoginInfo SsoLoginInfo { get; }

        /// <exclude/>
        bool CheckPasswordValid(string password);
    }
}
