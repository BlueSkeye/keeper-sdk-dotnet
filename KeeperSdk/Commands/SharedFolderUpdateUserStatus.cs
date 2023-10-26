using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class SharedFolderUpdateUserStatus
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}
