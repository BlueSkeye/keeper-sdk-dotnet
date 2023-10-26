using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class RecordUpdateStatus
    {
        [DataMember(Name = "record_uid")]
        public string RecordUid;

        [DataMember(Name = "status_code")]
        public string StatusCode;
    }
}
