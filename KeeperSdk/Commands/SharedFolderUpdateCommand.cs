using KeeperSecurity.Vault;
using System;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class SharedFolderUpdateCommand : AuthenticatedCommand, ISharedFolderAccessPath
    {
        public SharedFolderUpdateCommand() : base("shared_folder_update")
        {
            pt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        }

        [DataMember(Name = "pt", EmitDefaultValue = false)]
        public string pt;

        [DataMember(Name = "operation")]
        public string operation;

        [DataMember(Name = "shared_folder_uid")]
        public string shared_folder_uid;

        [DataMember(Name = "from_team_uid", EmitDefaultValue = false)]
        public string from_team_uid;

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string name;

        [DataMember(Name = "revision", EmitDefaultValue = false)]
        public long? revision;

        [DataMember(Name = "force_update", EmitDefaultValue = false)]
        public bool? forceUpdate;

        [DataMember(Name = "default_manage_users", EmitDefaultValue = false)]
        public bool? DefaultManageUsers { get; set; }

        [DataMember(Name = "default_manage_records", EmitDefaultValue = false)]
        public bool? DefaultManageRecords { get; set; }

        [DataMember(Name = "default_can_edit", EmitDefaultValue = false)]
        public bool? DefaultCanEdit { get; set; }

        [DataMember(Name = "default_can_share", EmitDefaultValue = false)]
        public bool? DefaultCanShare { get; set; }

        [DataMember(Name = "add_users", EmitDefaultValue = false)]
        public SharedFolderUpdateUser[] addUsers;

        [DataMember(Name = "update_users", EmitDefaultValue = false)]
        public SharedFolderUpdateUser[] updateUsers;

        [DataMember(Name = "remove_users", EmitDefaultValue = false)]
        public SharedFolderUpdateUser[] removeUsers;

        [DataMember(Name = "add_teams", EmitDefaultValue = false)]
        public SharedFolderUpdateTeam[] addTeams;

        [DataMember(Name = "update_teams", EmitDefaultValue = false)]
        public SharedFolderUpdateTeam[] updateTeams;

        [DataMember(Name = "remove_teams", EmitDefaultValue = false)]
        public SharedFolderUpdateTeam[] removeTeams;

        [DataMember(Name = "add_records", EmitDefaultValue = false)]
        public SharedFolderUpdateRecord[] addRecords;

        [DataMember(Name = "update_records", EmitDefaultValue = false)]
        public SharedFolderUpdateRecord[] updateRecords;

        [DataMember(Name = "remove_records", EmitDefaultValue = false)]
        public SharedFolderUpdateRecord[] removeRecords;

        public string SharedFolderUid
        {
            get => shared_folder_uid;
            set => shared_folder_uid = value;
        }

        public string TeamUid
        {
            get => from_team_uid;
            set => from_team_uid = value;
        }
    }
}
