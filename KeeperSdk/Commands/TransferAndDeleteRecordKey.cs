using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class TransferAndDeleteRecordKey
    {
        [DataMember(Name = "record_uid", EmitDefaultValue = false)]
        public string RecordUid { get; set; }

        [DataMember(Name = "record_key", EmitDefaultValue = false)]
        public string RecordKey { get; set; }
    }
}
