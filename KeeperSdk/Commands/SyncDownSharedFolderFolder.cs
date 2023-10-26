using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownSharedFolderFolder : IFolder
    {
        [DataMember(Name = "folder_uid")]
        public string FolderUid { get; set; }

        [DataMember(Name = "shared_folder_uid")]
        public string SharedFolderUid { get; set; }

        [DataMember(Name = "parent_uid")]
        public string ParentUid { get; set; }

        [DataMember(Name = "shared_folder_folder_key")]
        public string SharedFolderFolderKey;

        string IFolder.FolderKey => SharedFolderFolderKey;

        [DataMember(Name = "revision")]
        public long Revision { get; set; }

        [DataMember(Name = "type")]
        public string FolderType { get; set; }

        [DataMember(Name = "data")]
        public string Data { get; set; }

        string IUid.Uid => FolderUid;
    }
}
