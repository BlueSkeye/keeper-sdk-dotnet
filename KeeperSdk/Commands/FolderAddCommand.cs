using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class FolderAddCommand : FolderCommand
    {
        public FolderAddCommand() : base("folder_add")
        {
        }

        [DataMember(Name = "key", EmitDefaultValue = false)]
        public string Key { get; set; }
    }
}
