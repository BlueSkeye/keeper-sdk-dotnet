using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownFolderNode
    {
        [DataMember(Name = "folder_uid")]
        public string folderUid;
    }
}
