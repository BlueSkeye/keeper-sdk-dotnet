
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines common properties for Record Field
    /// </summary>
    public interface IRecordTypeField
    {
        /// <summary>
        /// Record Field Name
        /// </summary>
        string FieldName { get; }
        /// <summary>
        /// Record Field Label
        /// </summary>
        string FieldLabel { get; }
    }
}
