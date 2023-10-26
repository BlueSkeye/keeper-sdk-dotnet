using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownUserFolder : IFolder
    {
        [DataMember(Name = "folder_uid")]
        public string FolderUid { get; set; }

        [DataMember(Name = "parent_uid")]
        public string ParentUid { get; set; }

        [DataMember(Name = "user_folder_key")]
        public string FolderKey { get; set; }

        [DataMember(Name = "key_type")]
        public int keyType;

        [DataMember(Name = "revision")]
        public long Revision { get; set; }

        [DataMember(Name = "type")]
        public string FolderType { get; set; }

        [DataMember(Name = "data")]
        public string Data { get; set; }

        string IFolder.SharedFolderUid => null;

        string IUid.Uid => FolderUid;
    }
}
