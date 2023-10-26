using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class RecordShareUpdateResponse : KeeperApiResponse
    {
        [DataMember(Name = "add_statuses")]
        public RecordShareStatus[] AddStatuses;
        [DataMember(Name = "update_statuses")]
        public RecordShareStatus[] UpdateStatuses;
        [DataMember(Name = "remove_statuses")]
        public RecordShareStatus[] RemoveStatuses;
    }
}
