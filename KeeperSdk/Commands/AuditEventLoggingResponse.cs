using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class AuditEventLoggingResponse : KeeperApiResponse
    {
        [DataMember(Name = "ignored", EmitDefaultValue = false)]
        public AuditEventItem[] Ignored { get; set; }
    }
}
