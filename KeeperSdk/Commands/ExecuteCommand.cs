using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class ExecuteCommand : AuthenticatedCommand
    {
        public ExecuteCommand() : base("execute") { }

        [DataMember(Name = "requests", EmitDefaultValue = false)]
        public ICollection<KeeperApiCommand> Requests { get; set; }
    }
}
