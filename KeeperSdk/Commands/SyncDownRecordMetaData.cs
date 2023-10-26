using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownRecordMetaData : IRecordMetadata
    {
        [DataMember(Name = "record_uid")]
        public string RecordUid { get; set; }

        [DataMember(Name = "owner")]
        public bool Owner { get; set; }

        [DataMember(Name = "record_key")]
        public string RecordKey { get; set; }

        [DataMember(Name = "record_key_type")]
        public int RecordKeyType { get; set; }

        [DataMember(Name = "can_share")]
        public bool CanShare { get; set; }

        [DataMember(Name = "can_edit")]
        public bool CanEdit { get; set; }

        public string SharedFolderUid { get; set; }
        string IUidLink.SubjectUid => RecordUid;
        string IUidLink.ObjectUid => SharedFolderUid;
    }
}
