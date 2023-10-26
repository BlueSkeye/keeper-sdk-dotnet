
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents a Keeper Secret Manager Application Record.
    /// </summary>
    public class ApplicationRecord : KeeperRecord
    {
        /// <summary>
        /// Application Type.
        /// </summary>
        public string Type { get; set; }
    }
}
