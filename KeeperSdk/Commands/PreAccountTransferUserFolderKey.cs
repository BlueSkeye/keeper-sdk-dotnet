using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class PreAccountTransferUserFolderKey
    {
        [DataMember(Name = "user_folder_uid")]
        public string UserFolderUid { get; set; }

        [DataMember(Name = "user_folder_key")]
        public string UserFolderKey { get; set; }

        [DataMember(Name = "user_folder_key_type")]
        public int UserFolderKeyType { get; set; }
    }
}
