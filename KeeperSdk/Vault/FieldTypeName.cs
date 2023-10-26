using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// "name" field type
    /// </summary>
    [DataContract(Name = "Name")]
    public class FieldTypeName : IFieldTypeSerialize
    {
        /// <exclude />
        public FieldTypeName()
        {
            First = "";
            Middle = "";
            Last = "";
        }

        /// <summary>
        /// Gets or sets first name
        /// </summary>
        [DataMember(Name = "first", EmitDefaultValue = true)]
        public string First { get; set; }

        /// <summary>
        /// Gets or sets last name
        /// </summary>
        [DataMember(Name = "last", EmitDefaultValue = true)]
        public string Last { get; set; }

        /// <summary>
        /// Gets or sets middle name
        /// </summary>
        [DataMember(Name = "middle", EmitDefaultValue = true)]
        public string Middle { get; set; }

        private static readonly string[] NameElements = new string[] { "first", "middle", "last" };
        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.Elements => NameElements;

        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.ElementValues => new[] { First, Middle, Last };

        /// <exclude />
        bool IFieldTypeSerialize.SetElementValue(string element, string value)
        {
            if (element == "first")
            {
                First = value;
            }
            else if (element == "last")
            {
                Last = value;
            }
            else if (element == "middle")
            {
                Middle = value;
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
