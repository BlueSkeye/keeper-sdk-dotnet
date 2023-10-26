using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class EnterpriseAllocateIdsResponse : KeeperApiResponse
    {
        [DataMember(Name = "number_allocated")]
        public int NumberAllocated { get; set; }
        [DataMember(Name = "base_id")]
        public long BaseId { get; set; }
    }
}
