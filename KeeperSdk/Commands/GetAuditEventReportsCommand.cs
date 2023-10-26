using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class GetAuditEventReportsCommand : AuthenticatedCommand
    {
        public GetAuditEventReportsCommand() : base("get_audit_event_reports")
        {
        }

        [DataMember(Name = "report_type")]
        public string ReportType { get; set; } = "raw";

        [DataMember(Name = "scope")]
        public string Scope { get; internal set; } = "enterprise";

        [DataMember(Name = "order")]
        public string Order { get; set; } = "descending";

        [DataMember(Name = "limit")]
        public int Limit { get; set; } = 1000;

        [DataMember(Name = "filter", EmitDefaultValue = false)]
        public ReportFilter Filter { get; set; }
    }
}
