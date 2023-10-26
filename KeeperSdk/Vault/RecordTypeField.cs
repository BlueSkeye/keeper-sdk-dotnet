
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Record Types Schema: Record Field definition.
    /// </summary>
    public class RecordTypeField : IRecordTypeField
    {
        /// <summary>
        /// Initializes a new instance of the RecordTypeField class
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        public RecordTypeField(string fieldName) : this(fieldName, null)
        {
        }
        /// <summary>
        /// Initializes a new instance of the RecordTypeField class
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <param name="label">Field Label</param>
        public RecordTypeField(string fieldName, string label)
        {
            if (RecordTypesConstants.TryGetRecordField(fieldName, out var rf))
            {
                RecordField = rf;
            }
            FieldName = fieldName;
            FieldLabel = label;
        }
        /// <summary>
        /// Initializes a new instance of the RecordTypeField class
        /// </summary>
        /// <param name="recordField">Field</param>
        /// <param name="label">Field Label</param>
        public RecordTypeField(RecordField recordField, string label = null)
        {
            RecordField = recordField;
            FieldName = RecordField.Name;
            FieldLabel = label;
        }

        /// <summary>
        /// Gets Record Field
        /// </summary>
        public RecordField RecordField { get; }

        /// <summary>
        /// Gets field name
        /// </summary>
        public string FieldName { get; }
        /// <summary>
        /// Gets field label
        /// </summary>
        public string FieldLabel { get; }
    }
}
