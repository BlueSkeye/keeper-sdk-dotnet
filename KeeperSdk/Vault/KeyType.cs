
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Specifies key used for entity encryption.
    /// </summary>
    public enum KeyType
    {
        /// <summary>
        /// No entity key. Use data key.
        /// </summary>
        NoKey = 0,
        /// <summary>
        /// Key encrypted with the user data kay.
        /// </summary>
        DataKey = 1,
        /// <summary>
        /// Key is encrypted with the user RSA key.
        /// </summary>
        PrivateKey = 2,
        /// <summary>
        /// Key is encrypted with shared folder key.
        /// </summary>
        SharedFolderKey = 3,
        /// <summary>
        /// Key is encrypted with team key.
        /// </summary>
        TeamKey = 4,
        /// <summary>
        /// Key is encrypted with team RSA key.
        /// </summary>
        TeamPrivateKey = 5,
        /// <summary>
        /// Key is encrypted with record key.
        /// </summary>
        RecordKey = 6,
    }
}
