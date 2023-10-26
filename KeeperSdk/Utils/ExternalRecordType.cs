using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    [SqlTable(Name = "RecordType", PrimaryKey = new[] { "RecordTypeUid" })]
    [DataContract]
    public class ExternalRecordType : IEntity, IRecordType, IEntityCopy<IRecordType>
    {
        [SqlColumn(Length = 32)]
        [DataMember(Name = "record_type_uid", EmitDefaultValue = false)]
        public string RecordTypeUid { get; set; }

        [SqlColumn]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [SqlColumn]
        [DataMember(Name = "type_scope", EmitDefaultValue = false)]
        public int TypeScope { get; set; }

        [SqlColumn]
        [DataMember(Name = "content", EmitDefaultValue = false)]
        public string Content { get; set; }

        public string Uid
        {
            get => RecordTypeUid;
            set => RecordTypeUid = value;
        }

        RecordTypeScope IRecordType.Scope
        {
            get
            {
                switch (TypeScope)
                {
                    case (int) RecordTypeScope.Standard:
                        return RecordTypeScope.Standard;
                    case (int) RecordTypeScope.Enterprise:
                        return RecordTypeScope.Enterprise;
                    default:
                        return RecordTypeScope.User;
                }
            }
        }

        public void CopyFields(IRecordType source)
        {
            RecordTypeUid = source.Uid;
            Id = source.Id;
            TypeScope = (int) source.Scope;
            Content = source.Content;
        }
    }
}
