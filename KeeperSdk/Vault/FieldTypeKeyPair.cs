using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// "keyPair" field type
    /// </summary>
    [DataContract]
    public class FieldTypeKeyPair : IFieldTypeSerialize
    {
        public FieldTypeKeyPair()
        {
            PublicKey = "";
            PrivateKey = "";
        }
        /// <summary>
        /// Gets or sets Public Key
        /// </summary>
        [DataMember(Name = "publicKey", EmitDefaultValue = true)]
        public string PublicKey { get; set; }

        /// <summary>
        /// Gets or sets Private Key
        /// </summary>
        [DataMember(Name = "privateKey", EmitDefaultValue = true)]
        public string PrivateKey { get; set; }

        private static string[] KeyPairElements = new[] { "publicKey", "privateKey" };
        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.Elements => KeyPairElements;

        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.ElementValues => new[] { PublicKey, PrivateKey };

        /// <exclude />
        bool IFieldTypeSerialize.SetElementValue(string element, string value)
        {
            if (element == "publicKey")
            {
                PublicKey = value;
            }
            else if (element == "privateKey")
            {
                PrivateKey = value;
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
