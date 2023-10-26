
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines record access path properties.
    /// </summary>
    /// <remarks>
    /// Access to the record can be granted through:
    /// <list type="number">
    /// <item><description>Record is owned by user.</description></item>
    /// <item><description>Record is directly shared with user.</description></item>
    /// <item><description>Record is added to shared folder and user is a member of that shared folder.</description></item>
    /// <item><description>Record is added to shared folder and user is a member of team that is added that shared folder.</description></item>
    /// </list>
    /// </remarks>
    public interface IRecordAccessPath
    {
        /// <summary>
        /// Record UID.
        /// </summary>
        string RecordUid { get; }
        /// <summary>
        /// Shared Folder UID.
        /// </summary>
        string SharedFolderUid { get; set; }
        /// <summary>
        /// Team UID.
        /// </summary>
        string TeamUid { get; set; }
    }
}
