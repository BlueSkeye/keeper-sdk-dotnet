
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Specifies if Record Field allows multiple values.
    /// </summary>
    public enum RecordFieldMultiple
    {
        /// <summary>
        /// Single Value only
        /// </summary>
        None,
        /// <summary>
        /// Maybe multi-valued
        /// </summary>
        Optional,
        /// <summary>
        /// Multi-Value field
        /// </summary>
        Default,
    }
}
