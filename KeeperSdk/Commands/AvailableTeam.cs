using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class AvailableTeam
    {
        [DataMember(Name = "team_uid", EmitDefaultValue = false)]
        public string teamUid;
        [DataMember(Name = "team_name", EmitDefaultValue = false)]
        public string teamName;
    }
}
