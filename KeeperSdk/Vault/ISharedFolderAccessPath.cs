
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines shared folder access path properties.
    /// </summary>
    /// <remarks>
    /// Access to the shared folder can be granted through:
    /// <list type="number">
    /// <item><description>User is member of shared folder.</description></item>
    /// <item><description>User is member of team that is member of shared folder.</description></item>
    /// </list>
    /// </remarks>
    public interface ISharedFolderAccessPath
    {
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
