using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class RoleEnforcementUpdateCommand : RoleEnforcementCommand
    {
        public RoleEnforcementUpdateCommand() : base("role_enforcement_update")
        {
        }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
