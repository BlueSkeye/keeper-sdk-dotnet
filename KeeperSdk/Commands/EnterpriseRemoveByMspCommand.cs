using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class EnterpriseRemoveByMspCommand : AuthenticatedCommand
    {
        public EnterpriseRemoveByMspCommand() : base("enterprise_remove_by_msp")
        {
        }

        [DataMember(Name = "enterprise_id")]
        public int EnterpriseId { get; set; }
    }
}
