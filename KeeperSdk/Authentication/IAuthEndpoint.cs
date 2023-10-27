using KeeperSecurity.Utils;

namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Defines the basic properties of Keeper endpoint object.
    /// </summary>
    public interface IAuthEndpoint
    {
        /// <exclude/>
        IAuthCallback AuthCallback { get; }

        /// <summary>
        /// Gets a keeper server endpoint
        /// </summary>
        IKeeperEndpoint Endpoint { get; }
        /// <exclude/>
        IFanOut<NotificationEvent> PushNotifications { get; }

        /// <summary>
        /// Gets user email address.
        /// </summary>
        /// <remarks>
        /// This property is set by <c>Login</c> method
        /// </remarks>
        string Username { get; }

        /// <summary>
        /// Gets device token
        /// </summary>
        byte[] DeviceToken { get; }
    }
}
