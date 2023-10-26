using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    [SqlTable(Name = "SharedFolders", PrimaryKey = new[] { "SharedFolderUid" })]
    [DataContract]
    public class ExternalSharedFolder : IEntity, ISharedFolder, IEntityCopy<ISharedFolder>
    {
        [SqlColumn(Length = 32)]
        [DataMember(Name = "shared_folder_uid", EmitDefaultValue = false)]
        public string SharedFolderUid { get; set; }

        [SqlColumn]
        [DataMember(Name = "revision")]
        public long Revision { get; set; }

        [SqlColumn(Length = 256)]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [SqlColumn]
        [DataMember(Name = "default_manage_records")]
        public bool DefaultManageRecords { get; set; }

        [SqlColumn]
        [DataMember(Name = "default_manage_users")]
        public bool DefaultManageUsers { get; set; }

        [SqlColumn]
        [DataMember(Name = "default_can_edit")]
        public bool DefaultCanEdit { get; set; }

        [SqlColumn]
        [DataMember(Name = "default_can_share")]
        public bool DefaultCanShare { get; set; }

        public string Uid
        {
            get => SharedFolderUid;
            set => SharedFolderUid = value;
        }

        public void CopyFields(ISharedFolder source)
        {
            SharedFolderUid = source.Uid;
            Revision = source.Revision;
            Name = source.Name;
            DefaultManageRecords = source.DefaultManageRecords;
            DefaultManageUsers = source.DefaultManageUsers;
            DefaultCanEdit = source.DefaultCanEdit;
            DefaultCanShare = source.DefaultCanShare;
        }
    }
}
