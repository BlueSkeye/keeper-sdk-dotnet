
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Provides in memory implementation of IKeeperStorage interface.
    /// </summary>
    /// <seealso cref="IKeeperStorage" />
    public class InMemoryKeeperStorage : IKeeperStorage
    {
        public InMemoryKeeperStorage()
        {
            Clear();
        }

        /// <inheritdoc/>
        public string PersonalScopeUid { get; } = "PersonalScopeUid";

        /// <inheritdoc/>
        public long Revision { get; set; }

        /// <inheritdoc/>
        public IEntityStorage<IStorageRecord> Records { get; private set; }

        /// <inheritdoc/>
        public IEntityStorage<ISharedFolder> SharedFolders { get; private set; }

        /// <inheritdoc/>
        public IEntityStorage<IEnterpriseTeam> Teams { get; private set; }

        /// <inheritdoc/>
        public IEntityStorage<INonSharedData> NonSharedData { get; private set; }

        /// <inheritdoc/>
        public IPredicateStorage<IRecordMetadata> RecordKeys { get; private set; }

        /// <inheritdoc/>
        public IPredicateStorage<ISharedFolderKey> SharedFolderKeys { get; private set; }

        /// <inheritdoc/>
        public IPredicateStorage<ISharedFolderPermission> SharedFolderPermissions { get; private set; }

        /// <inheritdoc/>
        public IEntityStorage<IFolder> Folders { get; private set; }

        /// <inheritdoc/>
        public IPredicateStorage<IFolderRecordLink> FolderRecords { get; private set; }

        public IEntityStorage<IRecordType> RecordTypes { get; private set; }


        /// <inheritdoc/>
        public void Clear()
        {
            Records = new InMemoryItemStorage<IStorageRecord>();
            SharedFolders = new InMemoryItemStorage<ISharedFolder>();
            Teams = new InMemoryItemStorage<IEnterpriseTeam>();
            NonSharedData = new InMemoryItemStorage<INonSharedData>();
            RecordKeys = new InMemorySentenceStorage<IRecordMetadata>();
            SharedFolderKeys = new InMemorySentenceStorage<ISharedFolderKey>();
            SharedFolderPermissions = new InMemorySentenceStorage<ISharedFolderPermission>();
            Folders = new InMemoryItemStorage<IFolder>();
            FolderRecords = new InMemorySentenceStorage<IFolderRecordLink>();
            RecordTypes = new InMemoryItemStorage<IRecordType>();

            Revision = 0;
        }
    }
}
