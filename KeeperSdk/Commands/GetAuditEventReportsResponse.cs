using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class GetAuditEventReportsResponse : KeeperApiResponse
    {

        [DataMember(Name = "audit_event_overview_report_rows")]
        public List<Dictionary<string, object>> Events { get; private set; }
    }
}
