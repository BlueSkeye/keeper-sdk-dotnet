
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines properties record-folder link.
    /// </summary>
    public interface IFolderRecordLink : IUidLink
    {
        /// <summary>
        /// Folder UID.
        /// </summary>
        string FolderUid { get; }
        /// <summary>
        /// Record UID.
        /// </summary>
        string RecordUid { get; }
    }
}
