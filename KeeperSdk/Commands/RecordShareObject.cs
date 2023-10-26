using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class RecordShareObject : IRecordAccessPath
    {
        [DataMember(Name = "to_username")]
        public string ToUsername;

        [DataMember(Name = "record_key", EmitDefaultValue = false)]
        public string RecordKey;

        [DataMember(Name = "use_ecc_key", EmitDefaultValue = false)]
        public bool? useEccKey;

        [DataMember(Name = "editable", EmitDefaultValue = false)]
        public bool? Editable;

        [DataMember(Name = "shareable", EmitDefaultValue = false)]
        public bool? Shareable;

        [DataMember(Name = "transfer", EmitDefaultValue = false)]
        public bool? Transfer;

        [DataMember(Name = "record_uid", EmitDefaultValue = false)]
        public string RecordUid { get; set; }

        [DataMember(Name = "shared_folder_uid", EmitDefaultValue = false)]
        public string SharedFolderUid { get; set; }

        [DataMember(Name = "team_uid", EmitDefaultValue = false)]
        public string TeamUid { get; set; }
    }
}
