using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// "host" field type
    /// </summary>
    [DataContract]
    public class FieldTypeHost : IFieldTypeSerialize
    {
        /// <exclude />
        public FieldTypeHost()
        {
            HostName = "";
            Port = "";
        }
        /// <summary>
        /// Gets or sets hostname
        /// </summary>
        [DataMember(Name = "hostName", EmitDefaultValue = true)]
        public string HostName { get; set; }
        /// <summary>
        /// Gets or sets port
        /// </summary>
        [DataMember(Name = "port", EmitDefaultValue = true)]
        public string Port { get; set; }

        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.ElementValues => new[] { HostName, Port };

        private static readonly string[] HostElements = new[] { "hostName", "port" };
        /// <exclude />
        IEnumerable<string> IFieldTypeSerialize.Elements => HostElements;

        /// <exclude />
        bool IFieldTypeSerialize.SetElementValue(string element, string value)
        {
            if (element == "hostName")
            {
                HostName = value;
            }
            else if (element == "port")
            {
                Port = value;
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
