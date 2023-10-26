using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class MoveObject
    {
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string type;

        [DataMember(Name = "uid", EmitDefaultValue = false)]
        public string uid;

        [DataMember(Name = "from_type", EmitDefaultValue = false)]
        public string fromType;

        [DataMember(Name = "from_uid", EmitDefaultValue = false)]
        public string fromUid;

        [DataMember(Name = "can_edit")]
        public bool canEdit { get; set; }

        [DataMember(Name = "can_reshare")]
        public bool canShare { get; set; }

        [DataMember(Name = "cascade")]
        public bool cascade { get; set; }
    }
}
