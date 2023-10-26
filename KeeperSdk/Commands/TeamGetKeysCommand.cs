using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class TeamGetKeysCommand : AuthenticatedCommand
    {
        public TeamGetKeysCommand() : base("team_get_keys")
        {
        }

        [DataMember(Name = "teams", EmitDefaultValue = false)]
        public string[] teams;
    }
}
