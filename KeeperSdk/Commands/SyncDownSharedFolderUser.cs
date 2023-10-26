using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownSharedFolderUser : ISharedFolderPermission
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "manage_records")]
        public bool ManageRecords { get; set; }

        [DataMember(Name = "manage_users")]
        public bool ManageUsers { get; set; }

        public string SharedFolderUid { get; set; }

        string IUidLink.SubjectUid => SharedFolderUid;
        string IUidLink.ObjectUid => Username;
        string ISharedFolderPermission.UserId => Username;
        int ISharedFolderPermission.UserType => (int) UserType.User;
    }
}
