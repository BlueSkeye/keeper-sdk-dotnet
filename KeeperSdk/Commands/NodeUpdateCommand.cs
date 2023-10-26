using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class NodeUpdateCommand : NodeCommand
    {
        public NodeUpdateCommand() : base("node_update")
        {
        }
    }
}
