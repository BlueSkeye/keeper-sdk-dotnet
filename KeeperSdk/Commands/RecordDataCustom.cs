using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class RecordDataCustom
    {
        [DataMember(Name = "name")]
        public string name = "";

        [DataMember(Name = "value")]
        public string value = "";

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string type;
    }
}
