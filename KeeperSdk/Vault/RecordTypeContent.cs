using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    [DataContract]
    internal class RecordTypeContent
    {
        [DataMember(Name = "$id")]
        public string Name { get; set; }

        [DataMember(Name = "categories")]
        public string[] Categories { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "fields")]
        public RecordTypeContentField[] Fields { get; set; }
    }
}
