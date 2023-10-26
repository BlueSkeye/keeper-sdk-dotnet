using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    [SqlTable(Name = "NonSharedData", PrimaryKey = new[] { "RecordUid" })]
    [DataContract]
    public class ExternalNonSharedData : IEntity, INonSharedData, IEntityCopy<INonSharedData>
    {
        [SqlColumn(Length = 32)]
        [DataMember(Name = "record_uid", EmitDefaultValue = false)]
        public string RecordUid { get; set; }

        [SqlColumn]
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public string Data { get; set; }

        public void CopyFields(INonSharedData source)
        {
            RecordUid = source.RecordUid;
            Data = source.Data;
        }
        public string Uid
        {
            get => RecordUid;
            set => RecordUid = value;
        }
    }
}
