using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class RoleEnforcementCommand : AuthenticatedCommand
    {
        public RoleEnforcementCommand(string command) : base(command)
        {
        }

        [DataMember(Name = "role_id")]
        public long RoleId { get; set; }

        [DataMember(Name = "enforcement")]
        public string Enforcement { get; set; }
    }
}
