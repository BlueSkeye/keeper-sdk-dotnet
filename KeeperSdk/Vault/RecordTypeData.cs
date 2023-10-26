using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    [DataContract]
    internal class RecordTypeData
    {
        [DataMember(Name = "type", Order = 1)]
        public string Type { get; set; }

        [DataMember(Name = "title", Order = 2)]
        public string Title { get; set; }

        [DataMember(Name = "notes", Order = 3)]
        public string Notes { get; set; }

        [DataMember(Name = "fields", Order = 4)]
        public RecordTypeDataFieldBase[] Fields { get; set; }

        [DataMember(Name = "custom", Order = 5)]
        public RecordTypeDataFieldBase[] Custom { get; set; }
    }
}
