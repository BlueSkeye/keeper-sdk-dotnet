using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class ShareAccountCommand : AuthenticatedCommand
    {
        [DataMember(Name = "to_role_id")]
        public long ToRoleId;

        [DataMember(Name = "transfer_key")]
        public string TransferKey;

        public ShareAccountCommand() : base("share_account")
        {
        }
    }
}
