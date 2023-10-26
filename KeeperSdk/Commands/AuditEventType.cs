using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class AuditEventType
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "category", EmitDefaultValue = false)]
        public string Category { get; set; }

        [DataMember(Name = "critical", EmitDefaultValue = false)]
        public bool Critical { get; set; }

        [DataMember(Name = "syslog", EmitDefaultValue = false)]
        public string SyslogMessage { get; set; }
    }
}
