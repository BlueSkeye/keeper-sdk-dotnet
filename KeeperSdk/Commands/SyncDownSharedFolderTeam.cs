using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownSharedFolderTeam : ISharedFolderPermission
    {
        [DataMember(Name = "team_uid")]
        public string TeamUid { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "manage_records")]
        public bool ManageRecords { get; set; }

        [DataMember(Name = "manage_users")]
        public bool ManageUsers { get; set; }

        public string SharedFolderUid { get; set; }

        string ISharedFolderPermission.UserId => TeamUid;
        int ISharedFolderPermission.UserType => (int) UserType.Team;
        string IUidLink.SubjectUid => SharedFolderUid;
        string IUidLink.ObjectUid => TeamUid;
    }
}
