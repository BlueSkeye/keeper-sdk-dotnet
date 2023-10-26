using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class RecordDetail
    {
        [DataMember(Name = "record_uid")]
        public string RecordUid;

        [DataMember(Name = "revision")]
        public long Revision;

        [DataMember(Name = "version")]
        public int Version;

        [DataMember(Name = "shared")]
        public bool Shared;

        [DataMember(Name = "data")]
        public string Data;

        [DataMember(Name = "extra")]
        public string Extra;

        [DataMember(Name = "non_shared_data")]
        public string NonSharedData;

        [DataMember(Name = "user_permissions")]
        public RecordUserPermission[] UserPermissions;

        [DataMember(Name = "shared_folder_permissions")]
        public RecordSharedFolderPermission[] SharedFolderPermissions;
    }
}
