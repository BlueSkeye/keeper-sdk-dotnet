using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class TransitionKey
    {
        [DataMember(Name = "uid", EmitDefaultValue = false)]
        public string uid;

        [DataMember(Name = "key", EmitDefaultValue = false)]
        public string key;
    }
}
