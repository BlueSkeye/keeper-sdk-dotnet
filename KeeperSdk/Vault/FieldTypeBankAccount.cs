using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// "bankAccount" field type
    /// </summary>
    [DataContract]
    public class FieldTypeBankAccount : IFieldTypeSerialize
    {
        /// <exclude />
        public FieldTypeBankAccount()
        {
            AccountType = "";
            RoutingNumber = "";
            AccountNumber = "";
        }

        /// <summary>
        /// Gets or sets Account Type
        /// </summary>
        [DataMember(Name = "accountType", EmitDefaultValue = true)]
        public string AccountType { get; set; }

        /// <summary>
        /// Gets or sets Routing Number
        /// </summary>
        [DataMember(Name = "routingNumber", EmitDefaultValue = true)]
        public string RoutingNumber { get; set; }

        /// <summary>
        /// Gets or setsAccount Number
        /// </summary>
        [DataMember(Name = "accountNumber", EmitDefaultValue = true)]
        public string AccountNumber { get; set; }

        private static readonly string[] AccountElements = new[] { "accountType", "routingNumber", "accountNumber" };
        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.Elements => AccountElements;

        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.ElementValues => new[] { AccountType, RoutingNumber, AccountNumber };

        /// <exclude />
        bool IFieldTypeSerialize.SetElementValue(string element, string value)
        {
            if (element == "accountType")
            {
                AccountType = value;
            }
            else if (element == "routingNumber")
            {
                RoutingNumber = value;
            }
            else if (element == "accountNumber")
            {
                AccountNumber = value;
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
