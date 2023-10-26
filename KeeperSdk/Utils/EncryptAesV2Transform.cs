using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperSecurity.Utils
{
    /// <exclude />
    public class EncryptAesV2Transform : EncryptTransform
    {
        public EncryptAesV2Transform(byte[] key) : base(
            new BufferedAeadBlockCipher(new GcmBlockCipher(new AesEngine())), key, 12)
        {
        }
    }
}
