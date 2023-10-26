using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    [SqlTable(Name = "RecordKeys", PrimaryKey = new[] { "RecordUid", "SharedFolderUid" }, Index1 = new[] { "SharedFolderUid" })]
    [DataContract]
    public class ExternalRecordMetadata : IEntityLink, IRecordMetadata, IEntityCopy<IRecordMetadata>
    {
        [SqlColumn(Length = 32)]
        [DataMember(Name = "record_uid", EmitDefaultValue = false)]
        public string RecordUid { get; set; }

        [SqlColumn(Length = 32)]
        [DataMember(Name = "shared_folder_uid", EmitDefaultValue = false)]
        public string SharedFolderUid { get; set; }

        [SqlColumn]
        [DataMember(Name = "record_key", EmitDefaultValue = false)]
        public string RecordKey { get; set; }

        [SqlColumn]
        [DataMember(Name = "record_key_type")]
        public int RecordKeyType { get; set; }

        [SqlColumn]
        [DataMember(Name = "can_share")]
        public bool CanShare { get; set; }

        [SqlColumn]
        [DataMember(Name = "can_edit")]
        public bool CanEdit { get; set; }

        public string SubjectUid
        {
            get => RecordUid;
            set => RecordUid = value;
        }

        public string ObjectUid
        {
            get => SharedFolderUid;
            set => SharedFolderUid = value;
        }

        public void CopyFields(IRecordMetadata source)
        {
            RecordUid = source.RecordUid;
            SharedFolderUid = source.SharedFolderUid;
            RecordKeyType = source.RecordKeyType;
            RecordKey = source.RecordKey;
            CanShare = source.CanShare;
            CanEdit = source.CanEdit;
        }
    }
}
