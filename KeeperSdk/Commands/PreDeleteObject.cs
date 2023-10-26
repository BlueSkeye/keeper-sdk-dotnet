using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class PreDeleteObject
    {
        [DataMember(Name = "object_uid", EmitDefaultValue = false)]
        public string objectUid;

        [DataMember(Name = "object_type", EmitDefaultValue = false)]
        public string objectType;

        [DataMember(Name = "from_uid", EmitDefaultValue = false)]
        public string fromUid;

        [DataMember(Name = "from_type", EmitDefaultValue = false)]
        public string fromType;

        [DataMember(Name = "delete_resolution", EmitDefaultValue = false)]
        public string deleteResolution;
    }
}
