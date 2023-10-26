using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class RecordUserPermission
    {
        [DataMember(Name = "username")]
        public string Username;

        [DataMember(Name = "owner")]
        public bool Owner { get; set; }

        [DataMember(Name = "sharable")]
        public bool Sharable { get; set; }

        [DataMember(Name = "editable")]
        public bool Editable { get; set; }

        [DataMember(Name = "awaiting_approval")]
        public bool AwaitingApproval { get; set; }
    }
}
