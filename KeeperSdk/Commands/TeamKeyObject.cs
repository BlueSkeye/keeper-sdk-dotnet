using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class TeamKeyObject
    {
        [DataMember(Name = "team_uid")]
        public string teamUid;
        [DataMember(Name = "key")]
        public string key;
        [DataMember(Name = "type")]
        public int keyType;
        [DataMember(Name = "result_code")]
        public string resultCode;
        [DataMember(Name = "message")]
        public string message;
    }
}
