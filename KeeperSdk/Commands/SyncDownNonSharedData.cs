using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownNonSharedData : INonSharedData
    {
        [DataMember(Name = "record_uid")]
        public string recordUid;

        [DataMember(Name = "data")]
        public string data;

        public string RecordUid => recordUid;

        public string Data
        {
            get => data;
            set => data = value;
        }

        public string Uid => RecordUid;
    }
}
