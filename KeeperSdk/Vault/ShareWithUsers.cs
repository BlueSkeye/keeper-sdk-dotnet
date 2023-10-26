
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represent user list available for sharing
    /// </summary>
    public class ShareWithUsers
    {
        /// <summary>
        /// Array of users shared from
        /// </summary>
        public string[] SharesFrom { get; internal set; }
        /// <summary>
        /// Array of users shared to
        /// </summary>
        public string[] SharesWith { get; internal set; }
        /// <summary>
        /// Array of users in the enterprise
        /// </summary>
        public string[] GroupUsers { get; internal set; }
    }
}
