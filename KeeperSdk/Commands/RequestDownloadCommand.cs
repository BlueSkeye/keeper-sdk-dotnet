using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class RequestDownloadCommand : AuthenticatedCommand, IRecordAccessPath
    {
        public RequestDownloadCommand() : base("request_download")
        {
        }

        [DataMember(Name = "file_ids")]
        public string[] FileIDs;

        [DataMember(Name = "record_uid")]
        public string RecordUid { get; set; }

        [DataMember(Name = "shared_folder_uid", EmitDefaultValue = false)]
        public string SharedFolderUid { get; set; }

        [DataMember(Name = "team_uid", EmitDefaultValue = false)]
        public string TeamUid { get; set; }
    }
}
