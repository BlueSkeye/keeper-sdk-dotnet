using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class NodeDeleteCommand : AuthenticatedCommand
    {
        public NodeDeleteCommand() : base("node_delete")
        {
        }

        [DataMember(Name = "node_id")]
        public long NodeId { get; set; }

    }
}
