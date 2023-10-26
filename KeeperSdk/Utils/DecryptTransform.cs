using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KeeperSecurity.Utils
{
    /// <exclude />
    public class DecryptTransform : ICryptoTransform
    {
        private readonly IBufferedCipher _cypher;
        public readonly byte[] Key;
        private bool _initialized;

        public DecryptTransform(IBufferedCipher cypher, byte[] key, int ivSize)
        {
            _cypher = cypher;
            Key = key;
            _ivSize = ivSize > 0 ? ivSize : _cypher.GetBlockSize();
            _initialized = false;
            DecryptedBytes = 0;
        }

        public long DecryptedBytes { get; private set; }
        public int InputBlockSize => _cypher.GetBlockSize();

        public int OutputBlockSize => _cypher.GetBlockSize();

        public bool CanTransformMultipleBlocks => true;

        public bool CanReuseTransform => false;

        protected readonly int _ivSize;

        public void Dispose()
        {
        }

        private void EnsureInitialized(byte[] inputBuffer, ref int inputOffset, ref int inputCount)
        {
            if (!_initialized)
            {
                var iv = new byte[_ivSize];
                Array.Copy(inputBuffer, inputOffset, iv, 0, iv.Length);
                inputOffset += iv.Length;
                inputCount -= iv.Length;
                _cypher.Init(false, new ParametersWithIV(new KeyParameter(Key), iv));
                _initialized = true;
            }
        }

        public int TransformBlock(byte[] inputBuffer,
            int inputOffset,
            int inputCount,
            byte[] outputBuffer,
            int outputOffset)
        {
            EnsureInitialized(inputBuffer, ref inputOffset, ref inputCount);

            var res = _cypher.ProcessBytes(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
            DecryptedBytes += res;
            return res;
        }

        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            EnsureInitialized(inputBuffer, ref inputOffset, ref inputCount);

            var res = _cypher.DoFinal(inputBuffer, inputOffset, inputCount);
            DecryptedBytes += res.LongLength;
            return res;
        }
    }
}
