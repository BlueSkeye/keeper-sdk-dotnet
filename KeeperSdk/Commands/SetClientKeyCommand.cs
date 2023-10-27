using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class SetClientKeyCommand : AuthenticatedCommand
    {
        public SetClientKeyCommand() : base("set_client_key")
        {
        }

        [DataMember(Name = "client_key")]
        public string clientKey;
    }
}
