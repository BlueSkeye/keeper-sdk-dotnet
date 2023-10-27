using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class KeeperApiCommand
    {
        public KeeperApiCommand(string command)
        {
            this.command = command;
        }

        [DataMember(Name = "command", EmitDefaultValue = false)]
        public string command;

        [DataMember(Name = "locale", EmitDefaultValue = false)]
        public string locale = "en_US";

        [DataMember(Name = "client_version", EmitDefaultValue = false)]
        public string clientVersion;
    }
}
