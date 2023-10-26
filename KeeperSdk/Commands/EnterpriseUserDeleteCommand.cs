using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class EnterpriseUserDeleteCommand : EnterpriseUserCommand
    {
        public EnterpriseUserDeleteCommand() : base("enterprise_user_delete")
        {
        }
    }
}
