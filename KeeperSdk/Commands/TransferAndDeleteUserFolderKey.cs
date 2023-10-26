using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class TransferAndDeleteUserFolderKey
    {
        [DataMember(Name = "user_folder_uid", EmitDefaultValue = false)]
        public string UserFolderUid { get; set; }

        [DataMember(Name = "user_folder_key", EmitDefaultValue = false)]
        public string UserFolderKey { get; set; }
    }
}
