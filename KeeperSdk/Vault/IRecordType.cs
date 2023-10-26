
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines properties for Record Types.
    /// </summary>
    public interface IRecordType : IUid
    {
        /// <summary>
        /// Record Type ID
        /// </summary>
        int Id { get; }
        /// <summary>
        /// Record Type Scope
        /// </summary>
        RecordTypeScope Scope { get; }
        /// <summary>
        /// Record Type Content (JSON).
        /// </summary>
        string Content { get; }
    }
}
