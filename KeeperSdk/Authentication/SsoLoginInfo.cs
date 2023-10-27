
namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Describes SSO Provider connection parameters
    /// </summary>
    public class SsoLoginInfo
    {
        /// <summary>
        /// Gets SSO Provider name
        /// </summary>
        public string SsoProvider { get; internal set; }
        /// <summary>
        /// Gets SSO Provider base URL
        /// </summary>
        public string SpBaseUrl { get; internal set; }
        internal string IdpSessionId { get; set; }
    }
}
