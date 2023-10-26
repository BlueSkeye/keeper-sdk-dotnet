
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines properties for typed record field
    /// </summary>
    public interface ITypedField : IRecordTypeField, ICustomField
    {
        /// <summary>
        /// Gets or sets the first field value
        /// </summary>
        object ObjectValue { get; set; }

        /// <summary>
        /// Gets default field value.
        /// </summary>
        /// <returns></returns>
        object AppendValue();

        /// <summary>
        /// Gets value at index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns></returns>
        object GetValueAt(int index);

        /// <summary>
        /// Sets value at index.
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="value">Value</param>
        void SetValueAt(int index, object value);

        /// <summary>
        /// Deletes value at index.
        /// </summary>
        /// <param name="index">Index</param>
        void DeleteValueAt(int index);

        /// <summary>
        /// Gets the number of values
        /// </summary>
        int Count { get; }
    }
}
