using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class PreAccountTransferRecordKey
    {
        [DataMember(Name = "record_uid")]
        public string RecordUid { get; set; }

        [DataMember(Name = "record_key")]
        public string RecordKey { get; set; }

        [DataMember(Name = "record_key_type")]
        public int RecordKeyType { get; set; }
    }
}
