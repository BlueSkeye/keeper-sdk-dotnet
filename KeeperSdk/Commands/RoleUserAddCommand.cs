using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class RoleUserAddCommand : RoleUserCommand
    {
        public RoleUserAddCommand() : base("role_user_add")
        {
        }
        [DataMember(Name = "tree_key", EmitDefaultValue = false)]
        public string TreeKey { get; set; }

        [DataMember(Name = "role_admin_key", EmitDefaultValue = false)]
        public string RoleAdminKey { get; set; }
    }
}
