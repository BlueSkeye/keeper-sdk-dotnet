using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class ReportDimensions
    {

        [DataMember(Name = "audit_event_type")]
        public AuditEventType[] AuditEventTypes { get; set; }

    }
}
