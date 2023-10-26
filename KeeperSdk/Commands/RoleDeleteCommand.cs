using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class RoleDeleteCommand : AuthenticatedCommand
    {
        public RoleDeleteCommand() : base("role_delete")
        {
        }

        [DataMember(Name = "role_id")]
        public long RoleId { get; set; }
    }
}
