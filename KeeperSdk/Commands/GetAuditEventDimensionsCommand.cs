using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class GetAuditEventDimensionsCommand : AuthenticatedCommand
    {
        public GetAuditEventDimensionsCommand() : base("get_audit_event_dimensions")
        {
        }

        [DataMember(Name = "scope")]
        public string Scope { get; private set; } = "enterprise";

        [DataMember(Name = "columns")]
        public string[] Columns { get; private set; } = { "audit_event_type" };
    }
}
