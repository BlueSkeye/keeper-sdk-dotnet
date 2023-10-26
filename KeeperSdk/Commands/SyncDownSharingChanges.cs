using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownSharingChanges
    {
        [DataMember(Name = "record_uid")]
        public string recordUid;

        [DataMember(Name = "shared")]
        public bool shared;
    }
}
