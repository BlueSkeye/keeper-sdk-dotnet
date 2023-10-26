using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperSecurity.Utils
{
    /// <exclude />
    public class EncryptAesV1Transform : EncryptTransform
    {
        public EncryptAesV1Transform(byte[] key) : base(
            new PaddedBufferedBlockCipher(new CbcBlockCipher(new AesEngine()), new Pkcs7Padding()),
            key, 0)
        {
        }
    }
}
