using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class GetAvailableTeamsCommand : AuthenticatedCommand
    {
        public GetAvailableTeamsCommand() : base("get_available_teams")
        {
        }
    }
}
