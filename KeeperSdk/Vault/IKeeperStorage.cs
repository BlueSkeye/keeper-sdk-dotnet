
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines properties for offline Keeper vault storage.
    /// </summary>
    public interface IKeeperStorage
    {
        /// <summary>
        /// ID for logged in user. 
        /// </summary>
        string PersonalScopeUid { get; }

        /// <summary>
        /// Gets or sets revision.
        /// </summary>
        long Revision { get; set; }

        /// <summary>
        /// Gets record entity storage.
        /// </summary>
        IEntityStorage<IStorageRecord> Records { get; }

        /// <summary>
        /// Gets shared folder entity storage.
        /// </summary>
        IEntityStorage<ISharedFolder> SharedFolders { get; }

        /// <summary>
        /// Gets team entity storage.
        /// </summary>
        IEntityStorage<IEnterpriseTeam> Teams { get; }

        /// <summary>
        /// Gets non-shared record data entity storage.
        /// </summary>
        IEntityStorage<INonSharedData> NonSharedData { get; }

        /// <summary>
        /// Gets record key entity link storage.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><term>Object UID</term><description>Record UID</description></item>
        /// <item><term>Subject UID</term><description><c>PersonalScopeUid</c> or Shared Folder UID</description></item>
        /// </list>
        /// </remarks>
        IPredicateStorage<IRecordMetadata> RecordKeys { get; } // RecordUid / "" or SharedFolderUid

        /// <summary>
        /// Gets shared folder key entity link storage
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><term>Object UID</term><description>Shared Folder UID</description></item>
        /// <item><term>Subject UID</term><description><c>PersonalScopeUid</c> or Team UID</description></item>
        /// </list>
        /// </remarks>
        IPredicateStorage<ISharedFolderKey> SharedFolderKeys { get; }

        /// <summary>
        /// Gets shared folder user permission entity link storage.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><term>Object UID</term><description>Shared Folder UID</description></item>
        /// <item><term>Subject UID</term><description>User Email or Team UID</description></item>
        /// </list>
        /// </remarks>
        IPredicateStorage<ISharedFolderPermission> SharedFolderPermissions { get; }

        /// <summary>
        /// Gets folder entity storage.
        /// </summary>
        IEntityStorage<IFolder> Folders { get; }

        /// <summary>
        /// Gets folder's record entity link storage.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><term>Object UID</term><description>Folder UID</description></item>
        /// <item><term>Subject UID</term><description>Record UID</description></item>
        /// </list>
        /// </remarks>
        IPredicateStorage<IFolderRecordLink> FolderRecords { get; } // FolderUid / RecordUid

        /// <summary>
        /// Gets record type's entity storage
        /// </summary>
        IEntityStorage<IRecordType> RecordTypes { get; }

        /// <summary>
        /// Clear offline Keeper vault storage.
        /// </summary>
        void Clear();
    }
}
