using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// "paymentCard" field type
    /// </summary>
    [DataContract]
    public class FieldTypePaymentCard : IFieldTypeSerialize
    {
        /// <exclude />
        public FieldTypePaymentCard()
        {
            CardNumber = "";
            CardExpirationDate = "";
            CardSecurityCode = "";
        }
        /// <summary>
        /// Gets or sets Card Number
        /// </summary>
        [DataMember(Name = "cardNumber", EmitDefaultValue = true)]
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets Card Expiration Date
        /// </summary>
        [DataMember(Name = "cardExpirationDate", EmitDefaultValue = true)]
        public string CardExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets Card Security Code
        /// </summary>
        [DataMember(Name = "cardSecurityCode", EmitDefaultValue = true)]
        public string CardSecurityCode { get; set; }

        private static string[] CardElements = new[] { "cardNumber", "cardExpirationDate", "cardSecurityCode" };
        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.Elements => CardElements;

        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.ElementValues => new[] { CardNumber, CardExpirationDate, CardSecurityCode };

        /// <exclude />
        bool IFieldTypeSerialize.SetElementValue(string element, string value)
        {
            if (element == "cardNumber")
            {
                CardNumber = value;
            }
            else if (element == "cardExpirationDate")
            {
                CardExpirationDate = value;
            }
            else if (element == "cardSecurityCode")
            {
                CardSecurityCode = value;
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
