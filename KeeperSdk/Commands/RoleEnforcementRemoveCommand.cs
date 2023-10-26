using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class RoleEnforcementRemoveCommand : RoleEnforcementCommand
    {
        public RoleEnforcementRemoveCommand() : base("role_enforcement_remove")
        {
        }
    }
}
