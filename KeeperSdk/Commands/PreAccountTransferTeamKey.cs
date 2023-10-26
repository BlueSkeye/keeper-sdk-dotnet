using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class PreAccountTransferTeamKey
    {
        [DataMember(Name = "team_uid")]
        public string TeamUid { get; set; }

        [DataMember(Name = "team_key")]
        public string TeamKey { get; set; }

        [DataMember(Name = "team_key_type")]
        public int TeamKeyType { get; set; }
    }
}
