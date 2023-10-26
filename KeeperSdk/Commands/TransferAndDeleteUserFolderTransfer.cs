using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class TransferAndDeleteUserFolderTransfer
    {
        [DataMember(Name = "transfer_folder_uid", EmitDefaultValue = false)]
        public string TransferFolderUid { get; set; }

        [DataMember(Name = "transfer_parent_uid", EmitDefaultValue = false)]
        public string TransferParentUid { get; set; }

        [DataMember(Name = "transfer_folder_data", EmitDefaultValue = false)]
        public string TransferFolderData { get; set; }

        [DataMember(Name = "transfer_folder_key", EmitDefaultValue = false)]
        public string TransferFolderKey { get; set; }
    }
}
