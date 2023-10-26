using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class RecordSharedFolderPermission
    {
        [DataMember(Name = "shared_folder_uid")]
        public string SharedFolderUid;

        [DataMember(Name = "revision")]
        public long Revision;

        [DataMember(Name = "reshareable")]
        public bool Reshareable { get; set; }

        [DataMember(Name = "editable")]
        public bool Editable { get; set; }
    }
}
