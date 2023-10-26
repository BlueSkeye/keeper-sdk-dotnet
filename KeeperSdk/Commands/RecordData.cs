using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class RecordData
    {
        [DataMember(Name = "title")]
        public string title = "";

        [DataMember(Name = "folder")]
        public string folder = "";

        [DataMember(Name = "secret1")]
        public string secret1 = "";

        [DataMember(Name = "secret2")]
        public string secret2 = "";

        [DataMember(Name = "link")]
        public string link = "";

        [DataMember(Name = "notes")]
        public string notes = "";

        [DataMember(Name = "custom", EmitDefaultValue = false)]
        public RecordDataCustom[] custom;
    }
}
