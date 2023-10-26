using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class RecordShareStatus
    {
        [DataMember(Name = "username")]
        public string Username;
        [DataMember(Name = "record_uid")]
        public string RecordUid;
        [DataMember(Name = "status")]
        public string Status;
        [DataMember(Name = "message")]
        public string Message;
    }
}
