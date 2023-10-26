using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownSharedFolderRecord
    {
        [DataMember(Name = "record_uid")]
        public string RecordUid { get; set; }

        [DataMember(Name = "record_key")]
        public string RecordKey { get; set; }

        [DataMember(Name = "can_share")]
        public bool CanShare { get; set; }

        [DataMember(Name = "can_edit")]
        public bool CanEdit { get; set; }
    }
}
