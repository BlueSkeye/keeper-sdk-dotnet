using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class PreAccountTransferResponse : KeeperApiResponse
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "user_private_key")]
        public string UserPrivateKey { get; set; }

        [DataMember(Name = "user_ecc_private_key")]
        public string UserEccPrivateKey { get; set; }

        [DataMember(Name = "role_key")]
        public string RoleKey { get; set; }

        [DataMember(Name = "role_key_id")]
        public long? RoleKeyId { get; set; }

        [DataMember(Name = "role_private_key")]
        public string RolePrivateKey { get; set; }

        [DataMember(Name = "transfer_key")]
        public string TransferKey { get; set; }

        [DataMember(Name = "record_keys")]
        public PreAccountTransferRecordKey[] RecordKeys { get; set; }

        [DataMember(Name = "shared_folder_keys")]
        public PreAccountTransferSharedFolderKey[] SharedFolderKeys { get; set; }

        [DataMember(Name = "team_keys")]
        public PreAccountTransferTeamKey[] TeamKeys { get; set; }

        [DataMember(Name = "user_folder_keys")]
        public PreAccountTransferUserFolderKey[] UserFolderKeys { get; set; }
    }
}
