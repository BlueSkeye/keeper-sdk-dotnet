using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// "phone" field type
    /// </summary>
    [DataContract]
    public class FieldTypePhone : IFieldTypeSerialize
    {
        /// <exclude />
        public FieldTypePhone()
        {
            Region = "";
            Number = "";
            Ext = "";
            Type = "";
        }

        /// <summary>
        /// Gets or sets phone region
        /// </summary>
        [DataMember(Name = "region", EmitDefaultValue = true)]
        public string Region { get; set; }
        /// <summary>
        /// Gets or sets phone number
        /// </summary>
        [DataMember(Name = "number", EmitDefaultValue = true)]
        public string Number { get; set; }
        /// <summary>
        /// Gets or sets phone extension
        /// </summary>
        [DataMember(Name = "ext", EmitDefaultValue = true)]
        public string Ext { get; set; }
        /// <summary>
        /// Gets or sets phone type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = true)]
        public string Type { get; set; }

        private static readonly string[] PhoneElements = new[] { "region", "number", "ext", "type" };
        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.Elements => PhoneElements;

        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.ElementValues => new[] { Region, Number, Ext, Type };

        /// <exclude />
        bool IFieldTypeSerialize.SetElementValue(string element, string value)
        {
            if (element == "region")
            {
                Region = value;
            }
            else if (element == "number")
            {
                Number = value;
            }
            else if (element == "ext")
            {
                Ext = value;
            }
            else if (element == "type")
            {
                Type = value;
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
