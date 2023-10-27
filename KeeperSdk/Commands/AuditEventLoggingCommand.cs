using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class AuditEventLoggingCommand : AuthenticatedCommand
    {
        public AuditEventLoggingCommand() : base("audit_event_client_logging") { }

        [DataMember(Name = "item_logs", EmitDefaultValue = false)]
        public AuditEventItem[] ItemLogs { get; set; }
    }
}
