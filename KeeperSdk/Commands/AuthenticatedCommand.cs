using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class AuthenticatedCommand : KeeperApiCommand
    {
        public AuthenticatedCommand(string command) : base(command)
        {
        }

        [DataMember(Name = "device_id", EmitDefaultValue = false)]
        public string deviceId;

        [DataMember(Name = "session_token", EmitDefaultValue = false)]
        public string sessionToken;

        [DataMember(Name = "username", EmitDefaultValue = false)]
        public string username;
    }
}
