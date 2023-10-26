using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class RoleAddCommand : RoleCommand
    {
        public RoleAddCommand() : base("role_add")
        {
        }
    }
}
