using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownSharedFolderKey : ISharedFolderKey
    {
        [DataMember(Name = "shared_folder_uid")]
        public string SharedFolderUid { get; set; }

        [DataMember(Name = "shared_folder_key")]
        public string SharedFolderKey { get; set; }

        [DataMember(Name = "key_type")]
        public int KeyType { get; set; }

        public string TeamUid { get; set; }

        string IUidLink.SubjectUid => SharedFolderUid;
        string IUidLink.ObjectUid => TeamUid;
    }
}
