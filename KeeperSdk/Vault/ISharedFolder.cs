
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines properties for shared folder.
    /// </summary>
    public interface ISharedFolder : IUid
    {
        /// <summary>
        /// Shared folder UID.
        /// </summary>
        string SharedFolderUid { get; }
        /// <exclude/>
        long Revision { get; }
        /// <summary>
        /// Shared folder name. Encrypted with the shared folder key.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Can manage records by default?
        /// </summary>
        bool DefaultManageRecords { get; }
        /// <summary>
        /// Can manage users by default?
        /// </summary>
        bool DefaultManageUsers { get; }
        /// <summary>
        /// Can edit records by default?
        /// </summary>
        bool DefaultCanEdit { get; }
        /// <summary>
        /// Can re-share records by default.
        /// </summary>
        bool DefaultCanShare { get; }
    }
}
