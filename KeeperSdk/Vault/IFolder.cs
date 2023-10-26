
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines properties for folder.
    /// </summary>
    public interface IFolder : IUid
    {
        /// <summary>
        /// Parent folder UID.
        /// </summary>
        string ParentUid { get; }
        /// <summary>
        /// Folder UID.
        /// </summary>
        string FolderUid { get; }
        /// <summary>
        /// Folder type.
        /// </summary>
        string FolderType { get; }
        /// <summary>
        /// Folder key. Encrypted with data key for <c>user_folder</c> or <c>shared folder key</c> for <c>shared_folder_folder</c>
        /// </summary>
        string FolderKey { get; }
        /// <summary>
        /// Shared Folder UID.
        /// </summary>
        string SharedFolderUid { get; }
        /// <summary>
        /// Revision.
        /// </summary>
        long Revision { get; }
        /// <summary>
        /// Shared folder data. Encrypted with the shared folder key.
        /// </summary>
        string Data { get; }
    }
}
