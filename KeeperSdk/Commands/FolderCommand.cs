using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class FolderCommand : AuthenticatedCommand
    {
        public FolderCommand(string command) : base(command)
        {
        }

        [DataMember(Name = "folder_uid", EmitDefaultValue = false)]
        public string FolderUid { get; set; }

        [DataMember(Name = "folder_type", EmitDefaultValue = false)]
        public string FolderType { get; set; }

        [DataMember(Name = "parent_uid", EmitDefaultValue = false)]
        public string ParentUid { get; set; }

        [DataMember(Name = "shared_folder_uid", EmitDefaultValue = false)]
        public string SharedFolderUid { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "data", EmitDefaultValue = false)]
        public string Data { get; set; }

        [DataMember(Name = "manage_users", EmitDefaultValue = false)]
        public bool? ManageUsers { get; set; }

        [DataMember(Name = "manage_records", EmitDefaultValue = false)]
        public bool? ManageRecords { get; set; }

        [DataMember(Name = "can_edit", EmitDefaultValue = false)]
        public bool? CanEdit { get; set; }

        [DataMember(Name = "can_share", EmitDefaultValue = false)]
        public bool? CanShare { get; set; }
    }
}
