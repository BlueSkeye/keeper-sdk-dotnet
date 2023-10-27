using KeeperSecurity.Configuration;
using KeeperSecurity.Utils;
using System;
using System.Threading.Tasks;

namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Defines the properties and methods of not connected Keeper authentication object.
    /// </summary>
    public interface IAuth : IAuthEndpoint
    {
        /// <summary>
        /// Gets or sets username.
        /// </summary>
        new string Username { get; set; }

        /// <exclude />
        void SetPushNotifications(IFanOut<NotificationEvent> pushNotifications);

        /// <summary>
        /// Gets or sets device token
        /// </summary>
        new byte[] DeviceToken { get; set; }

        /// <summary>
        /// Gets configuration storage 
        /// </summary>
        IConfigurationStorage Storage { get; }

        /// <summary>
        /// Gets or sets session resumption flag
        /// </summary>
        bool ResumeSession { get; set; }

        /// <summary>
        /// Forces master password login for SSO accounts.
        /// </summary>
        bool AlternatePassword { get; set; }

        /// <summary>
        /// Login to Keeper account with email.
        /// </summary>
        /// <param name="username">Keeper account email address.</param>
        /// <param name="passwords">Master password(s)</param>
        /// <returns>Awaitable task</returns>
        /// <seealso cref="LoginSso(string, bool)"/>
        /// <exception cref="KeeperStartLoginException">Unrecoverable login error.</exception>
        /// <exception cref="KeeperCanceled">Login cancelled.</exception>
        /// <exception cref="Exception">Other exceptions.</exception>
        Task Login(string username, params string[] passwords);

        /// <summary>
        /// Login to Keeper account with SSO Provider.
        /// </summary>
        /// <param name="providerName">SSO provider name.</param>
        /// <param name="forceLogin">Force new login with SSO IdP.</param>
        /// <returns>Awaitable task.</returns>
        /// <exception cref="KeeperStartLoginException">Unrecoverable login error.</exception>
        /// <exception cref="KeeperCanceled">Login cancelled.</exception>
        /// <exception cref="Exception">Other exceptions.</exception>
        /// <seealso cref="Login(string, string[])" />
        Task LoginSso(string providerName, bool forceLogin = false);
    }
}
