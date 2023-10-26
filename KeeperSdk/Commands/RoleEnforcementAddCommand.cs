using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class RoleEnforcementAddCommand : RoleEnforcementCommand
    {
        public RoleEnforcementAddCommand() : base("role_enforcement_add")
        {
        }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
