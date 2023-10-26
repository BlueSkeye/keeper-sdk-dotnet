
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represent record sharing information
    /// </summary>
    public class RecordSharePermissions
    {
        /// <summary>
        /// Record UID
        /// </summary>
        public string RecordUid { get; internal set; }
        /// <summary>
        /// List of direct record share permissions
        /// </summary>
        public UserRecordPermissions[] UserPermissions { get; internal set; }
        /// <summary>
        /// List of shared folder permissions
        /// </summary>
        public SharedFolderRecordPermissions[] SharedFolderPermissions { get; internal set; }
    }
}
