using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownResponse : KeeperApiResponse
    {
        [DataMember(Name = "full_sync")]
        public bool fullSync;

        [DataMember(Name = "revision")]
        public long revision;

        [DataMember(Name = "records")]
        public SyncDownRecord[] records;

        [DataMember(Name = "shared_folders")]
        public SyncDownSharedFolder[] sharedFolders;

        [DataMember(Name = "teams")]
        public SyncDownTeam[] teams;

        [DataMember(Name = "non_shared_data")]
        public SyncDownNonSharedData[] nonSharedData;

        [DataMember(Name = "record_meta_data")]
        public SyncDownRecordMetaData[] recordMetaData;

        [DataMember(Name = "pending_shares_from")]
        public string[] pendingSharesFrom;

        [DataMember(Name = "sharing_changes")]
        public SyncDownSharingChanges[] sharingChanges;

        [DataMember(Name = "removed_shared_folders")]
        public string[] removedSharedFolders;

        [DataMember(Name = "removed_records")]
        public string[] removedRecords;

        [DataMember(Name = "removed_teams")]
        public string[] removedTeams;

        [DataMember(Name = "removed_links")]
        public SyncDownRecordLink[] removedLinks;

        [DataMember(Name = "user_folders")]
        public SyncDownUserFolder[] userFolders;

        [DataMember(Name = "user_folder_records")]
        public SyncDownFolderRecord[] userFolderRecords;

        [DataMember(Name = "user_folders_removed")]
        public SyncDownFolderNode[] userFoldersRemoved;

        [DataMember(Name = "user_folders_removed_records")]
        public SyncDownFolderRecordNode[] userFoldersRemovedRecords;

        [DataMember(Name = "user_folder_shared_folders")]
        public SyncDownUserFolderSharedFolder[] userFolderSharedFolders;

        [DataMember(Name = "user_folder_shared_folders_removed")]
        public SyncDownUserFolderSharedFolder[] userFolderSharedFoldersRemoved;

        [DataMember(Name = "shared_folder_folders")]
        public SyncDownSharedFolderFolder[] sharedFolderFolders;

        [DataMember(Name = "shared_folder_folder_removed")]
        public SyncDownSharedFolderFolderNode[] sharedFolderFolderRemoved;

        [DataMember(Name = "shared_folder_folder_records")]
        public SyncDownSharedFolderFolderRecordNode[] sharedFolderFolderRecords;

        [DataMember(Name = "shared_folder_folder_records_removed")]
        public SyncDownSharedFolderFolderRecordNode[] sharedFolderFolderRecordsRemoved;
    }
}
