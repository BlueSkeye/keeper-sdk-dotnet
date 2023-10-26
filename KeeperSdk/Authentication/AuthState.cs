
namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Specifies authentication states.
    /// </summary>
    public enum AuthState
    {
        /// <summary>
        /// Ready to login
        /// </summary>
        NotConnected,
        /// <summary>
        /// Device Approval
        /// </summary>
        DeviceApproval,
        /// <summary>
        /// Two Factor Authentication
        /// </summary>
        TwoFactor,
        /// <summary>
        /// Master Password
        /// </summary>
        Password,
        /// <summary>
        /// SSO Login
        /// </summary>
        SsoToken,
        /// <summary>
        /// SSO Approval
        /// </summary>
        SsoDataKey,
        /// <summary>
        /// Login success
        /// </summary>
        Connected,
        /// <summary>
        /// Login failure
        /// </summary>
        Error,
        /// <summary>
        /// Restricted Connection
        /// </summary>
        Restricted,
    }
}
