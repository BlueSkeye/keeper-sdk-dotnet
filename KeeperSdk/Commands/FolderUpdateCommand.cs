using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class FolderUpdateCommand : FolderCommand
    {
        public FolderUpdateCommand() : base("folder_update")
        {
        }

        [DataMember(Name = "team_uid", EmitDefaultValue = false)]
        public string TeamUid { get; set; }
    }
}
