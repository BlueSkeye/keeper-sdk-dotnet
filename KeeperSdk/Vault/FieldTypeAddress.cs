using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// "address" field type
    /// </summary>
    [DataContract]
    public class FieldTypeAddress : IFieldTypeSerialize
    {
        /// <exclude />
        public FieldTypeAddress()
        {
            Street1 = "";
            Street2 = "";
            City = "";
            State = "";
            Zip = "";
            Country = "";
        }

        /// <summary>
        /// Gets or sets Street 1
        /// </summary>
        [DataMember(Name = "street1", EmitDefaultValue = true)]
        public string Street1 { get; set; }

        /// <summary>
        /// Gets or sets Street 1
        /// </summary>
        [DataMember(Name = "street2", EmitDefaultValue = true)]
        public string Street2 { get; set; }

        /// <summary>
        /// Gets or sets City
        /// </summary>
        [DataMember(Name = "city", EmitDefaultValue = true)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets State
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = true)]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets Zip/Postal Code
        /// </summary>
        [DataMember(Name = "zip", EmitDefaultValue = true)]
        public string Zip { get; set; }

        /// <summary>
        /// Gets or sets Country
        /// </summary>
        [DataMember(Name = "country", EmitDefaultValue = true)]
        public string Country { get; set; }

        private static readonly string[] AddressElements = new string[] { "street1", "street2", "city", "state", "zip", "country" };
        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.Elements => AddressElements;

        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.ElementValues => new[] { Street1, Street2, City, State, Zip, Country };

        /// <exclude />
        bool IFieldTypeSerialize.SetElementValue(string element, string value)
        {
            if (element == "street1")
            {
                Street1 = value;
            }
            else if (element == "street2")
            {
                Street2 = value;
            }
            else if (element == "city")
            {
                City = value;
            }
            else if (element == "state")
            {
                State = value;
            }
            else if (element == "zip")
            {
                Zip = value;
            }
            else if (element == "country")
            {
                Country = value;
            }
            else
            {
                return false;
            }

            return true;
        }

    }
}
