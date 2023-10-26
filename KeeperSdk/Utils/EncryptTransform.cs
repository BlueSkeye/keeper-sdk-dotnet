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
    public class EncryptTransform : ICryptoTransform
    {
        private readonly IBufferedCipher _cypher;
        private byte[] _tail;

        public EncryptTransform(IBufferedCipher cypher, byte[] key, int ivSize = 0)
        {
            _cypher = cypher;

            var iv = CryptoUtils.GetRandomBytes(ivSize > 0 ? ivSize : _cypher.GetBlockSize());
            _cypher.Init(true, new ParametersWithIV(new KeyParameter(key), iv));
            _tail = iv;
            EncryptedBytes = 0;
        }

        public long EncryptedBytes { get; private set; }
        public int InputBlockSize => _cypher.GetBlockSize();

        public int OutputBlockSize => _cypher.GetBlockSize();

        public bool CanTransformMultipleBlocks => true;

        public bool CanReuseTransform => false;

        public void Dispose()
        {
        }

        public int TransformBlock(byte[] inputBuffer,
            int inputOffset,
            int inputCount,
            byte[] outputBuffer,
            int outputOffset)
        {
            EncryptedBytes += inputCount;
            var encrypted = _cypher.ProcessBytes(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
            if (_tail.Length > 0)
            {
                if (_tail.Length <= outputBuffer.Length - (outputOffset + encrypted))
                {
                    Array.Copy(outputBuffer, outputOffset, outputBuffer, outputOffset + _tail.Length, encrypted);
                    Array.Copy(_tail, 0, outputBuffer, outputOffset, _tail.Length);
                    encrypted += _tail.Length;
                    _tail = new byte[0];
                }
                else
                {
                    if (_tail.Length <= encrypted)
                    {
                        var newTail = new byte[_tail.Length];
                        Array.Copy(outputBuffer, outputOffset + encrypted - _tail.Length, newTail, 0, _tail.Length);
                        Array.Copy(outputBuffer, outputOffset, outputBuffer, outputOffset + _tail.Length, encrypted - newTail.Length);
                        Array.Copy(_tail, 0, outputBuffer, outputOffset, _tail.Length);
                        _tail = newTail;
                    }
                    else
                    {
                        var newTail = new byte[_tail.Length + encrypted];
                        Array.Copy(_tail, 0, newTail, 0, _tail.Length);
                        Array.Copy(outputBuffer, outputOffset, newTail, _tail.Length, encrypted);
                        _tail = newTail;
                        encrypted = 0;
                    }
                }
            }

            return encrypted;
        }

        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            EncryptedBytes += inputCount;
            var final = _cypher.DoFinal(inputBuffer, inputOffset, inputCount);
            var result = new byte[_tail.Length + final.Length];
            Array.Copy(_tail, 0, result, 0, _tail.Length);
            Array.Copy(final, 0, result, _tail.Length, final.Length);
            return result;
        }
    }
}
