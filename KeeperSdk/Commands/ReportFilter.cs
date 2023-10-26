using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class ReportFilter
    {
        [DataMember(Name = "audit_event_type", EmitDefaultValue = false)]
        public string[] EventTypes { get; set; }

        [DataMember(Name = "username", EmitDefaultValue = false)]
        public string Username { get; set; }

        [DataMember(Name = "to_username", EmitDefaultValue = false)]
        public string ToUsername { get; set; }

        [DataMember(Name = "record_uid", EmitDefaultValue = false)]
        public string RecordUid { get; set; }

        [DataMember(Name = "shared_folder_uid", EmitDefaultValue = false)]
        public string SharedFolderUid { get; set; }

        [DataMember(Name = "created", EmitDefaultValue = false)]
        public object Created { get; set; }
    }
}
