using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class TeamUpdateCommand : TeamCommand
    {
        public TeamUpdateCommand() : base("team_update")
        {
        }
    }
}
