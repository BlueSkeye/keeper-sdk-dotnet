using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class AuditEventInput
    {
        [DataMember(Name = "record_uid", EmitDefaultValue = false)]
        public string RecordUid { get; set; }

        [DataMember(Name = "attachment_id", EmitDefaultValue = false)]
        public string AttachmentId { get; set; }
    }
}
