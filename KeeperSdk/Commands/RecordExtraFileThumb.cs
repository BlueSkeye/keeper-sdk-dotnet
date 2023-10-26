using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class RecordExtraFileThumb
    {
        [DataMember(Name = "id")]
        public string id = "";

        [DataMember(Name = "type")]
        public string type = "";

        [DataMember(Name = "size")]
        public int? size;
    }
}
