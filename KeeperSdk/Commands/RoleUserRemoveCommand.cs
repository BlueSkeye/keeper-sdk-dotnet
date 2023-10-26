using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class RoleUserRemoveCommand : RoleUserCommand
    {
        public RoleUserRemoveCommand() : base("role_user_remove")
        {
        }
    }
}
