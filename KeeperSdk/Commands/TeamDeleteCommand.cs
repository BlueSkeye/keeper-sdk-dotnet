using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class TeamDeleteCommand : AuthenticatedCommand
    {
        public TeamDeleteCommand() : base("team_delete")
        {
        }
        [DataMember(Name = "team_uid")]
        public string TeamUid { get; set; }
    }
}
