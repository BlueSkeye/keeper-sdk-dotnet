using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class TransferAndDeleteUserCommand : AuthenticatedCommand
    {
        public TransferAndDeleteUserCommand() : base("transfer_and_delete_user") { }

        [DataMember(Name = "from_user", EmitDefaultValue = false)]
        public string FromUser { get; set; }

        [DataMember(Name = "to_user", EmitDefaultValue = false)]
        public string ToUser { get; set; }

        [DataMember(Name = "record_keys", EmitDefaultValue = false)]
        public TransferAndDeleteRecordKey[] RecordKeys { get; set; }

        [DataMember(Name = "shared_folder_keys", EmitDefaultValue = false)]
        public TransferAndDeleteSharedFolderKey[] SharedFolderKeys { get; set; }

        [DataMember(Name = "team_keys", EmitDefaultValue = false)]
        public TransferAndDeleteTeamKey[] TeamKeys { get; set; }

        [DataMember(Name = "user_folder_keys", EmitDefaultValue = false)]
        public TransferAndDeleteUserFolderKey[] UserFolderKeys { get; set; }

        [DataMember(Name = "corrupted_record_keys")]
        public PreAccountTransferRecordKey[] CorruptedRecordKeys { get; set; }
        [DataMember(Name = "corrupted_shared_folder_keys")]
        public PreAccountTransferSharedFolderKey[] CorruptedSharedFolderKeys { get; set; }
        [DataMember(Name = "corrupted_team_keys")]
        public PreAccountTransferTeamKey[] CorruptedTeamKeys { get; set; }
        [DataMember(Name = "corrupted_user_folder_keys")]
        public PreAccountTransferUserFolderKey[] CorruptedUserFolderKeys { get; set; }

        [DataMember(Name = "user_folder_transfer")]
        public TransferAndDeleteUserFolderTransfer UserFolderTransfer { get; set; }
    }
}
