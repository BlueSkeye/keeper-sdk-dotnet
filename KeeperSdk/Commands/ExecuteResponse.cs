using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class ExecuteResponse : KeeperApiResponse
    {
        [DataMember(Name = "results")]
        public IList<KeeperApiResponse> Results { get; set; }
    }
}
