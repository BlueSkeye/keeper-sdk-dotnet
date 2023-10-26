using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class RoleCommand : AuthenticatedCommand
    {
        public RoleCommand(string command) : base(command)
        {
        }

        [DataMember(Name = "role_id")]
        public long RoleId { get; set; }

        [DataMember(Name = "node_id")]
        public long NodeId { get; set; }

        [DataMember(Name = "encrypted_data")]
        public string EncryptedData { get; set; }

        [DataMember(Name = "visible_below")]
        public bool VisibleBelow { get; set; }

        [DataMember(Name = "new_user_inherit")]
        public bool NewUserInherit { get; set; }
    }
}
