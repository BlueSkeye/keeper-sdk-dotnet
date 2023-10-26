using System;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class GetRecordsCommand : AuthenticatedCommand
    {
        public GetRecordsCommand() : base("get_records")
        {
        }

        [DataMember(Name = "include", EmitDefaultValue = false)]
        public string[] Include;

        [DataMember(Name = "client_time")]
        public long ClientTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        [DataMember(Name = "records", EmitDefaultValue = false)]
        public RecordAccessPath[] Records;
    }
}
