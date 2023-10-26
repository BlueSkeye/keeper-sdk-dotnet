
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents shared folder record permissions.
    /// </summary>
    public class SharedFolderRecord
    {
        /// <summary>
        /// Record UID.
        /// </summary>
        public string RecordUid { get; internal set; }
        /// <summary>
        /// Can be re-shared?
        /// </summary>
        public bool CanShare { get; internal set; }
        /// <summary>
        /// Can be edited?
        /// </summary>
        public bool CanEdit { get; internal set; }
    }
}
