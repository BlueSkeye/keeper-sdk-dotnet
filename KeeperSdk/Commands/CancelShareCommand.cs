using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class CancelShareCommand : AuthenticatedCommand
    {
        public CancelShareCommand()
            : base("cancel_share")
        {
        }

        [DataMember(Name = "from_email")]
        public string FromEmail;

        [DataMember(Name = "to_email")]
        public string ToEmail;
    }
}
