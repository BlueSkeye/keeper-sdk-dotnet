using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class UserPublicKeysObject
    {
        [DataMember(Name = "key_owner", EmitDefaultValue = false)]
        public string keyOwner;
        [DataMember(Name = "public_key", EmitDefaultValue = false)]
        public string publicKey;
        [DataMember(Name = "result_code", EmitDefaultValue = false)]
        public string resultCode;
        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string message;
    }
}
