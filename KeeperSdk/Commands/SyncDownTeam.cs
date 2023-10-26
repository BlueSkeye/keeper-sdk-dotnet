using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownTeam : IEnterpriseTeam
    {
        [DataMember(Name = "team_uid")]
        public string TeamUid { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "team_key")]
        public string TeamKey { get; set; }

        [DataMember(Name = "team_key_type")]
        public int KeyType { get; set; }

        [DataMember(Name = "team_private_key")]
        public string TeamPrivateKey { get; set; }

        [DataMember(Name = "restrict_edit")]
        public bool RestrictEdit { get; set; }

        [DataMember(Name = "restrict_share")]
        public bool RestrictShare { get; set; }

        [DataMember(Name = "restrict_view")]
        public bool RestrictView { get; set; }

        [DataMember(Name = "removed_shared_folders")]
        public string[] removedSharedFolders;

        [DataMember(Name = "shared_folder_keys")]
        public SyncDownSharedFolderKey[] sharedFolderKeys;

        string IUid.Uid => TeamUid;
    }
}
