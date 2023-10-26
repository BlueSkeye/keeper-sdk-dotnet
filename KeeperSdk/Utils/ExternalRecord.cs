using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    [SqlTable(Name = "Records", PrimaryKey = new[] { "RecordUid" })]
    [DataContract]
    public class ExternalRecord : IEntity, IStorageRecord, IEntityCopy<IStorageRecord>
    {
        [SqlColumn(Length = 32)]
        [DataMember(Name = "record_uid", EmitDefaultValue = false)]
        public string RecordUid { get; set; }

        [SqlColumn]
        [DataMember(Name = "revision", EmitDefaultValue = false)]
        public long Revision { get; set; }

        [SqlColumn]
        [DataMember(Name = "version", EmitDefaultValue = false)]
        public int Version { get; set; }

        [SqlColumn]
        [DataMember(Name = "client_modified_time", EmitDefaultValue = false)]
        public long ClientModifiedTime { get; set; }

        [SqlColumn]
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public string Data { get; set; }

        [SqlColumn]
        [DataMember(Name = "extra", EmitDefaultValue = false)]
        public string Extra { get; set; }

        [SqlColumn]
        [DataMember(Name = "udata", EmitDefaultValue = false)]
        public string Udata { get; set; }

        [SqlColumn]
        [DataMember(Name = "shared")]
        public bool Shared { get; set; }

        [SqlColumn]
        [DataMember(Name = "owner")]
        public bool Owner { get; set; }

        public string Uid
        {
            get => RecordUid;
            set => RecordUid = value;
        }

        public void CopyFields(IStorageRecord source)
        {
            RecordUid = source.RecordUid;
            Revision = source.Revision;
            ClientModifiedTime = source.ClientModifiedTime;
            Data = source.Data;
            Extra = source.Extra;
            Udata = source.Udata;
            Shared = source.Shared;
            Owner = source.Owner;
        }
    }
}
