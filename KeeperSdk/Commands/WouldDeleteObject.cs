using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class WouldDeleteObject
    {
        [DataMember(Name = "deletion_summary", EmitDefaultValue = false)]
        public string[] deletionSummary;
    }
}
