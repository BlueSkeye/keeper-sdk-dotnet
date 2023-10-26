using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    [SqlTable(Name = "FolderRecords", PrimaryKey = new[] { "FolderUid", "RecordUid" }, Index1 = new[] { "RecordUid" })]
    [DataContract]
    public class ExternalFolderRecordLink : IEntityLink, IFolderRecordLink, IEntityCopy<IFolderRecordLink>
    {
        [SqlColumn(Length = 32)]
        [DataMember(Name = "folder_uid", EmitDefaultValue = false)]
        public string FolderUid { get; set; }

        [SqlColumn(Length = 32)]
        [DataMember(Name = "record_uid", EmitDefaultValue = false)]
        public string RecordUid { get; set; }

        public string SubjectUid
        {
            get => FolderUid;
            set => FolderUid = value;
        }

        public string ObjectUid
        {
            get => RecordUid;
            set => RecordUid = value;
        }

        public void CopyFields(IFolderRecordLink source)
        {
            FolderUid = source.FolderUid;
            RecordUid = source.RecordUid;
        }
    }
}
