using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    [SqlTable(Name = "SharedFolderUsers", PrimaryKey = new[] { "SharedFolderUid", "UserId" }, Index1 = new[] { "UserId" })]
    [DataContract]
    public class ExternalSharedFolderPermission : IEntityLink, ISharedFolderPermission, IEntityCopy<ISharedFolderPermission>
    {
        [SqlColumn(Length = 32)]
        [DataMember(Name = "shared_folder_uid", EmitDefaultValue = false)]
        public string SharedFolderUid { get; set; }

        [SqlColumn(Length = 64)]
        [DataMember(Name = "user_id", EmitDefaultValue = false)]
        public string UserId { get; set; }

        [SqlColumn]
        [DataMember(Name = "user_type")]
        public int UserType { get; set; }

        [SqlColumn]
        [DataMember(Name = "manage_records")]
        public bool ManageRecords { get; set; }

        [SqlColumn]
        [DataMember(Name = "manage_users")]
        public bool ManageUsers { get; set; }

        public string SubjectUid
        {
            get => SharedFolderUid;
            set => SharedFolderUid = value;
        }

        public string ObjectUid
        {
            get => UserId;
            set => UserId = value;
        }

        public void CopyFields(ISharedFolderPermission source)
        {
            SharedFolderUid = source.SharedFolderUid;
            UserId = source.UserId;
            UserType = source.UserType;
            ManageRecords = source.ManageRecords;
            ManageUsers = source.ManageUsers;
        }
    }
}
