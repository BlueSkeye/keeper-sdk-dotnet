
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Record Types Schema: Field definition.
    /// </summary>
    public class RecordField
    {
        /// <exclude />
        public RecordField(string name, FieldType fieldType, RecordFieldMultiple multiple = RecordFieldMultiple.None)
        {
            Name = name;
            Type = fieldType;
            Multiple = multiple;
        }

        /// <summary>
        /// Record Field Name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Field Type
        /// </summary>
        public FieldType Type { get; }
        /// <summary>
        /// Multi-Value attribute
        /// </summary>
        public RecordFieldMultiple Multiple { get; }
    }
}
