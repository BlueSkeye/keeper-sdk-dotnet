
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Specifies folder types.
    /// </summary>
    public enum FolderType
    {
        /// <summary>
        /// User folder.
        /// </summary>
        UserFolder,
        /// <summary>
        /// Shared folder.
        /// </summary>
        SharedFolder,
        /// <summary>
        /// Subfolder of shared folder.
        /// </summary>
        /// <remarks><see cref="SharedFolderFolder"/> inherits user and record permissions from the parent shared folder.</remarks>
        SharedFolderFolder
    }
}
