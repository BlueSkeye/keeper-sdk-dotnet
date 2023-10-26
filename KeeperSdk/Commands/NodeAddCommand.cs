using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class NodeAddCommand : NodeCommand
    {
        public NodeAddCommand() : base("node_add")
        {
        }
    }
}
