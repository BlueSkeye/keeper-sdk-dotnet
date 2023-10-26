using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownFolderRecordNode
    {
        [DataMember(Name = "folder_uid")]
        public string folderUid;

        [DataMember(Name = "record_uid")]
        public string recordUid;

        public string FolderUid => folderUid;
        public string RecordUid => recordUid;
    }
}
