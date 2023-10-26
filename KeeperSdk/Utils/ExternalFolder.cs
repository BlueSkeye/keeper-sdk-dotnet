using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    [SqlTable(Name = "Folders", PrimaryKey = new[] { "FolderUid" })]
    [DataContract]
    public class ExternalFolder : IEntity, IFolder, IEntityCopy<IFolder>
    {
        [SqlColumn(Length = 32)]
        [DataMember(Name = "folder_uid", EmitDefaultValue = false)]
        public string FolderUid { get; set; }

        [SqlColumn]
        [DataMember(Name = "revision")]
        public long Revision { get; set; }

        [SqlColumn(Length = 32)]
        [DataMember(Name = "parent_uid", EmitDefaultValue = false)]
        public string ParentUid { get; set; }

        [SqlColumn]
        [DataMember(Name = "folder_type", EmitDefaultValue = false)]
        public string FolderType { get; set; }

        [SqlColumn]
        [DataMember(Name = "folder_key", EmitDefaultValue = false)]
        public string FolderKey { get; set; }

        [SqlColumn(Length = 32)]
        [DataMember(Name = "shared_folder_uid", EmitDefaultValue = false)]
        public string SharedFolderUid { get; set; }

        [SqlColumn]
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public string Data { get; set; }

        public string Uid
        {
            get => FolderUid;
            set => FolderUid = value;
        }

        public void CopyFields(IFolder source)
        {
            FolderUid = source.FolderUid;
            Revision = source.Revision;
            ParentUid = source.ParentUid;
            FolderType = source.FolderType;
            FolderKey = source.FolderKey;
            SharedFolderUid = source.SharedFolderUid;
            Data = source.Data;
        }
    }
}
