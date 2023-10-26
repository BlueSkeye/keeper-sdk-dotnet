using System;
using System.Threading.Tasks;

namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Represents Master Password step
    /// </summary>
    public class PasswordStep : AuthStep
    {
        internal PasswordStep() : base(AuthState.Password)
        {
        }

        internal Func<string, Task> onPassword;

        /// <summary>
        /// Verifies master password
        /// </summary>
        /// <param name="password">Master password</param>
        /// <returns>Awaitable task</returns>
        public Task VerifyPassword(string password)
        {
            return onPassword?.Invoke(password);
        }

        internal Func<byte[], Task> onBiometricKey;
        /// <summary>
        /// Verifies biometric key
        /// </summary>
        /// <param name="biometricKey">Biometric key</param>
        /// <returns>Awaitable task</returns>
        public Task VerifyBiometricKey(byte[] biometricKey)
        {
            return onBiometricKey?.Invoke(biometricKey);
        }
    }
}
