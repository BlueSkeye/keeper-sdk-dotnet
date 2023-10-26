using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class GetAvailableTeamsResponse : KeeperApiResponse
    {
        [DataMember(Name = "teams", EmitDefaultValue = false)]
        public AvailableTeam[] teams;
    }
}
