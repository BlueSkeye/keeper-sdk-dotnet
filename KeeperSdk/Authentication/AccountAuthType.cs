
namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Specifies login type
    /// </summary>
    public enum AccountAuthType
    {
        /// <summary>
        /// Regular account
        /// </summary>
        Regular = 1,
        /// <summary>
        /// Cloud SSO account
        /// </summary>
        CloudSso = 2,
        /// <summary>
        /// On-Premises SSO account
        /// </summary>
        OnsiteSso = 3,
        /// <summary>
        /// MSP logged in to MC
        /// </summary>
        ManagedCompany = 4
    }
}
