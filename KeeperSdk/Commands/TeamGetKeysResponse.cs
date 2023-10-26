using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class TeamGetKeysResponse : KeeperApiResponse
    {
        [DataMember(Name = "keys", EmitDefaultValue = false)]
        public TeamKeyObject[] keys;
    }
}
