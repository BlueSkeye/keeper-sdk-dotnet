using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class PublicKeysCommand : AuthenticatedCommand
    {
        public PublicKeysCommand() : base("public_keys")
        {
        }

        [DataMember(Name = "key_owners", EmitDefaultValue = false)]
        public string[] keyOwners;
    }
}
