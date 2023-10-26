using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class FolderData
    {
        [DataMember(Name = "name")]
        public string name;
    }
}
