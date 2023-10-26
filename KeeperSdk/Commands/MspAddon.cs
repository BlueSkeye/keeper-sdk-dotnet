using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class MspAddon
    {
        [DataMember(Name = "seats", EmitDefaultValue = false)]
        public int? Seats { get; set; }

        [DataMember(Name = "add_on")]
        public string AddOn { get; set; }
    }
}
