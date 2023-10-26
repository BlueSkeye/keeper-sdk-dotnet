using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class RecordUpdateResponse : KeeperApiResponse
    {
        [DataMember(Name = "add_records")]
        public RecordUpdateStatus[] AddRecords;

        [DataMember(Name = "update_records")]
        public RecordUpdateRecord[] UpdateRecords;

        [DataMember(Name = "remove_records")]
        public RecordUpdateStatus[] RemoveRecords;

        [DataMember(Name = "delete_records")]
        public RecordUpdateStatus[] DeleteRecords;

        [DataMember(Name = "revision")]
        public long Revision;
    }
}
