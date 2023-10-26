using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownSharedFolder : ISharedFolder
    {
        [DataMember(Name = "shared_folder_uid")]
        public string SharedFolderUid { get; set; }

        [DataMember(Name = "revision")]
        public long Revision { get; set; }

        [DataMember(Name = "shared_folder_key")]
        public string SharedFolderKey { get; set; }

        [DataMember(Name = "key_type")]
        public int? KeyType { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "full_sync")]
        public bool? fullSync;

        [DataMember(Name = "manage_records")]
        public bool? ManageRecords { get; set; }

        [DataMember(Name = "manage_users")]
        public bool? ManageUsers { get; set; }

        [DataMember(Name = "default_manage_records")]
        public bool DefaultManageRecords { get; set; }

        [DataMember(Name = "default_manage_users")]
        public bool DefaultManageUsers { get; set; }

        [DataMember(Name = "default_can_edit")]
        public bool DefaultCanEdit { get; set; }

        [DataMember(Name = "default_can_share")]
        public bool DefaultCanShare { get; set; }

        [DataMember(Name = "records")]
        public SyncDownSharedFolderRecord[] records;

        [DataMember(Name = "users")]
        public SyncDownSharedFolderUser[] users;

        [DataMember(Name = "teams")]
        public SyncDownSharedFolderTeam[] teams;

        [DataMember(Name = "records_removed")]
        public string[] recordsRemoved;

        [DataMember(Name = "users_removed")]
        public string[] usersRemoved;

        [DataMember(Name = "teams_removed")]
        public string[] teamsRemoved;

        string IUid.Uid => SharedFolderUid;
    }
}
