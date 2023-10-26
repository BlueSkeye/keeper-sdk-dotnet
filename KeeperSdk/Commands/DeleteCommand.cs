using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class DeleteCommand : AuthenticatedCommand
    {
        public DeleteCommand() : base("delete")
        {
        }

        [DataMember(Name = "pre_delete_token", EmitDefaultValue = false)]
        public string preDeleteToken;
    }
}
