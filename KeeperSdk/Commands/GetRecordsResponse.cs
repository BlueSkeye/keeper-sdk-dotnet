using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class GetRecordsResponse : KeeperApiResponse
    {
        [DataMember(Name = "records", EmitDefaultValue = false)]
        public RecordDetail[] Records;
    }
}
