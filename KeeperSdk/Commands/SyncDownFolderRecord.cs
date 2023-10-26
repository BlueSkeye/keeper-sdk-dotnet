using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownFolderRecord : IFolderRecordLink
    {
        [DataMember(Name = "folder_uid")]
        public string FolderUid { get; set; }

        [DataMember(Name = "record_uid")]
        public string RecordUid { get; set; }

        [DataMember(Name = "revision")]
        public long revision;

        string IUidLink.SubjectUid => FolderUid;
        string IUidLink.ObjectUid => RecordUid;
    }
}
