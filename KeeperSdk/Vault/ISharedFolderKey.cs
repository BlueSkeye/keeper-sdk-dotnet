
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines shared folder key properties.
    /// </summary>
    public interface ISharedFolderKey : IUidLink
    {
        /// <summary>
        /// Shared Folder UID.
        /// </summary>
        string SharedFolderUid { get; }
        /// <summary>
        /// Team Uid if shared folder UID is encrypted with team key.
        /// </summary>
        string TeamUid { get; }
        /// <summary>
        /// Shared folder key encryption key type.
        /// </summary>
        int KeyType { get; }
        /// <summary>
        /// Encrypted shared folder key.
        /// </summary>
        string SharedFolderKey { get; }
    }
}
