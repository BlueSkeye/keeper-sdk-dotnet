using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class AddFolderResponse : KeeperApiResponse
    {
        [DataMember(Name = "revision")]
        public long revision;
    }
}
