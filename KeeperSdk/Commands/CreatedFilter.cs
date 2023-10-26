using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class CreatedFilter
    {
        [DataMember(Name = "max", EmitDefaultValue = false)]
        public long? Max { get; set; }

        [DataMember(Name = "min", EmitDefaultValue = false)]
        public long? Min { get; set; }

        [DataMember(Name = "exclude_max")]
        public bool ExcludeMax { get; set; } = true;

        [DataMember(Name = "exclude_min")]
        public bool ExcludeMin { get; set; }
    }
}
