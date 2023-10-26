using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownSharedFolderFolderRecordNode : IFolderRecordLink
    {
        [DataMember(Name = "folder_uid")]
        public string folderUid;

        [DataMember(Name = "record_uid")]
        public string recordUid;

        [DataMember(Name = "shared_folder_uid")]
        public string sharedFolderUid;

        string IFolderRecordLink.FolderUid => folderUid ?? sharedFolderUid;
        string IFolderRecordLink.RecordUid => recordUid;

        string IUidLink.SubjectUid => folderUid ?? sharedFolderUid;
        string IUidLink.ObjectUid => recordUid;
    }
}
