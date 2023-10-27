using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class AuditEventItem
    {
        [DataMember(Name = "audit_event_type", EmitDefaultValue = false)]
        public string AuditEventType { get; set; }

        [DataMember(Name = "inputs", EmitDefaultValue = false)]
        public AuditEventInput Inputs { get; set; }

        [DataMember(Name = "event_time", EmitDefaultValue = false)]
        public long? EventTime { get; set; }

    }
}
