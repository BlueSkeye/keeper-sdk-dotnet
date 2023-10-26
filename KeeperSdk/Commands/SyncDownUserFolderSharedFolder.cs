using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownUserFolderSharedFolder : IFolder
    {
        [DataMember(Name = "folder_uid")]
        public string folderUid;

        [DataMember(Name = "shared_folder_uid")]
        public string SharedFolderUid { get; set; }

        public string FolderUid => SharedFolderUid;
        string IFolder.ParentUid => folderUid ?? "";
        string IFolder.FolderType => "shared_folder";
        string IFolder.FolderKey => null;
        long IFolder.Revision => 0;
        string IFolder.Data => null;

        string IUid.Uid => FolderUid;
    }
}
