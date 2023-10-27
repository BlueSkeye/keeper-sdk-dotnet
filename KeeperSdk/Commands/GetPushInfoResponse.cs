using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class GetPushInfoResponse : KeeperApiResponse
    {
        [DataMember(Name = "url")]
        public string url;
    }
}
