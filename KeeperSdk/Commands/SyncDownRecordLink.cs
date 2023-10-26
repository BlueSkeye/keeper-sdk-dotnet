using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownRecordLink : IUidLink
    {
        [DataMember(Name = "record_uid")]
        public string recordUid;

        [DataMember(Name = "owner_uid")]
        public string ownerUid;

        string IUidLink.SubjectUid => ownerUid;

        string IUidLink.ObjectUid => recordUid;
    }
}
