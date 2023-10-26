
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents a custom field.
    /// </summary>
    public class CustomField : ICustomField
    {
        /// <summary>
        /// Custom field name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Custom field value.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Custom field type.
        /// </summary>
        public string Type { get; set; }
    }
}
