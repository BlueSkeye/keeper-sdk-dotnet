using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class SetClientKeyResponse : KeeperApiResponse
    {
        [DataMember(Name = "client_key")]
        public string clientKey;
    }
}
