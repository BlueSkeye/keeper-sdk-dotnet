using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class RoleUpdateCommand : RoleCommand
    {
        public RoleUpdateCommand() : base("role_update")
        {
        }
    }
}
