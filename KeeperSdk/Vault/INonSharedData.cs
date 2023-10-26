
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines non-shared data properties.
    /// </summary>
    public interface INonSharedData : IUid
    {
        /// <summary>
        /// Record UID.
        /// </summary>
        string RecordUid { get; }
        /// <summary>
        /// Encrypted record data.
        /// </summary>
        string Data { get; set; }
    }
}
