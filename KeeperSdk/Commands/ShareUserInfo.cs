using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class ShareUserInfo
    {
        [DataMember(Name = "email")]
        public string Email { get; set; }
    }
}
