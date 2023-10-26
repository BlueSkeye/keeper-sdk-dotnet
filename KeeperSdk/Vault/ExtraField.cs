using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents an extra field.
    /// </summary>
    public class ExtraField
    {
        /// <summary>
        /// Extra field ID.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Extra field type.
        /// </summary>
        public string FieldType { get; set; }
        /// <summary>
        /// Extra field title.
        /// </summary>
        public string FieldTitle { get; set; }
        /// <summary>
        /// Additional extra field values.
        /// </summary>
        public Dictionary<string, object> Custom { get; } = new Dictionary<string, object>();
    }
}
