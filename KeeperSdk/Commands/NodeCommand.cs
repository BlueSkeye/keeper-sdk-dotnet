using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class NodeCommand : AuthenticatedCommand
    {
        public NodeCommand(string command) : base(command)
        {
        }

        [DataMember(Name = "node_id")]
        public long NodeId { get; set; }

        [DataMember(Name = "parent_id", EmitDefaultValue = false)]
        public long? ParentId { get; set; }

        [DataMember(Name = "encrypted_data")]
        public string EncryptedData { get; set; }
    }
}
