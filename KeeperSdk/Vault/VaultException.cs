using System;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// The exception that is thrown by the Vault module.
    /// </summary>
    public class VaultException : Exception
    {
        /// <exclude/>
        public VaultException(string message) : base(message)
        {
        }
        /// <exclude/>
        public VaultException(string translationKey, string message) : base(message)
        {
            TranslationKey = translationKey;
        }

        /// <exclude />
        public string TranslationKey { get; }
    }
}
