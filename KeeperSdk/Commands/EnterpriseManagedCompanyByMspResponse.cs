using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class EnterpriseManagedCompanyByMspResponse : KeeperApiResponse
    {
        [DataMember(Name = "enterprise_id")]
        public int EnterpriseId { get; set; }
    }
}
