using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class PreDeleteCommand : AuthenticatedCommand
    {
        public PreDeleteCommand() : base("pre_delete")
        {
        }

        [DataMember(Name = "objects", EmitDefaultValue = false)]
        public PreDeleteObject[] objects;
    }
}
