using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class GetAuditEventDimensionsResponse : KeeperApiResponse
    {

        [DataMember(Name = "dimensions")]
        public ReportDimensions Dimensions { get; private set; }
    }
}
