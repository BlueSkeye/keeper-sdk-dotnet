using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class PublicKeysResponse : KeeperApiResponse
    {
        [DataMember(Name = "public_keys", EmitDefaultValue = false)]
        public UserPublicKeysObject[] publicKeys;
    }
}
