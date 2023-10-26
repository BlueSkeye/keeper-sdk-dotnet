using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class TeamEnterpriseUserRemoveCommand : TeamEnterpriseUserCommand
    {
        public TeamEnterpriseUserRemoveCommand() : base("team_enterprise_user_remove")
        {
        }
    }
}
