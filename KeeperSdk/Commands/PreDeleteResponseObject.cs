using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class PreDeleteResponseObject
    {
        [DataMember(Name = "pre_delete_token", EmitDefaultValue = false)]
        public string preDeleteToken;

        [DataMember(Name = "would_delete", EmitDefaultValue = false)]
        public WouldDeleteObject wouldDelete;
    }
}
