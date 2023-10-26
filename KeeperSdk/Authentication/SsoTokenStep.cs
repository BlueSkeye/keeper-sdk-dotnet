using System;
using System.Threading.Tasks;

namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Represents SSO Login step
    /// </summary>
    public class SsoTokenStep : AuthStep
    {
        internal SsoTokenStep() : base(AuthState.SsoToken)
        {
        }

        /// <summary>
        /// Gets username used to login
        /// </summary>
        public string LoginAsUser { get; internal set; }

        /// <summary>
        /// Gets SSO provider used to login
        /// </summary>
        public bool LoginAsProvider { get; internal set; }

        /// <summary>
        /// Gets SSO login URL
        /// </summary>
        public string SsoLoginUrl { get; internal set; }

        /// <summary>
        /// Gets cloud SSO flag
        /// </summary>
        public bool IsCloudSso { get; internal set; }

        internal Func<string, Task> OnSetSsoToken;
        /// <summary>
        /// Sets SSO login token
        /// </summary>
        /// <param name="ssoToken">SSO token</param>
        /// <returns></returns>
        public Task SetSsoToken(string ssoToken)
        {
            return OnSetSsoToken?.Invoke(ssoToken);
        }

        internal Func<Task> OnLoginWithPassword;
        /// <summary>
        /// Login with alternate Keeper password
        /// </summary>
        /// <returns></returns>
        public Task LoginWithPassword()
        {
            return OnLoginWithPassword?.Invoke();
        }
    }
}
