using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class GetPushInfoCommand : AuthenticatedCommand
    {
        public GetPushInfoCommand() : base("get_push_info")
        {
        }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string type;
    }
}
