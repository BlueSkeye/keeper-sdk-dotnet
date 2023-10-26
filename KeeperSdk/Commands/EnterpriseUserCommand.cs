using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class EnterpriseUserCommand : AuthenticatedCommand
    {
        public EnterpriseUserCommand(string command) : base(command)
        {
        }

        [DataMember(Name = "enterprise_user_id")]
        public long EnterpriseUserId { get; set; }
    }
}
