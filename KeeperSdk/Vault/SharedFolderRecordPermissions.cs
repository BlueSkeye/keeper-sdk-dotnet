
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents record permissions in shared folder.
    /// </summary>
    public class SharedFolderRecordPermissions
    {
        /// <summary>
        /// Shared Folder UID.
        /// </summary>
        public string SharedFolderUid { get; internal set; }
        /// <summary>
        /// Flag indicating if the shared folder has share permissions.
        /// </summary>
        public bool CanShare { get; internal set; }
        /// <summary>
        /// Flag indicating if the shared folder has rights to edit the record
        /// </summary>
        public bool CanEdit { get; internal set; }
    }
}
