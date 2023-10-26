using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class EnterpriseAllocateIdsCommand : AuthenticatedCommand
    {
        public EnterpriseAllocateIdsCommand() : base("enterprise_allocate_ids")
        {
            NumberRequested = 5;
        }

        [DataMember(Name = "number_requested")]
        public long NumberRequested { get; set; }
    }
}
