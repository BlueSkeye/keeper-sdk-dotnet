using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class RecordExtraFile
    {
        [DataMember(Name = "id")]
        public string id = "";

        [DataMember(Name = "name")]
        public string name = "";
        [DataMember(Name = "key")]
        public string key;

        [DataMember(Name = "size", EmitDefaultValue = false)]
        public long? size;

        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string title;

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string type;

        [DataMember(Name = "lastModified", EmitDefaultValue = false)]
        public long? lastModified;

        [DataMember(Name = "thumbs")]
        public RecordExtraFileThumb[] thumbs;
    }
}
