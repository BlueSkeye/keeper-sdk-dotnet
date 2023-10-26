using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents team properties that user is member of.
    /// </summary>
    public class Team : TeamInfo
    {
        /// <summary>
        /// Team restricts record edit.
        /// </summary>
        public bool RestrictEdit { get; set; }
        /// <summary>
        /// Team restricts record re-share.
        /// </summary>
        public bool RestrictShare { get; set; }
        /// <summary>
        /// Team restricts record view.
        /// </summary>
        public bool RestrictView { get; set; }

        /// <summary>
        /// Team key.
        /// </summary>
        public byte[] TeamKey { get; set; }

        /// <summary>
        /// Team RSA private key.
        /// </summary>
        public RsaPrivateCrtKeyParameters TeamPrivateKey { get; internal set; }
    }
}
