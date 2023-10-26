using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeeperSecurity.Authentication.Async
{
    /// <summary>
    /// Defines the user interface methods required for authentication with Keeper.
    /// </summary>
    /// <seealso cref="IAuthSsoUI"/>
    /// <seealso cref="IHttpProxyCredentialUi"/>
    /// <seealso cref="IAuthSecurityKeyUI"/>
    /// <seealso cref="IPostLoginTaskUI"/>
    public interface IAuthUI : IAuthCallback
    {
        /// <summary>
        /// Device Approval is required.
        /// </summary>
        /// <param name="channels">List of available device approval channels.</param>
        /// <param name="token">Cancellation token. Keeper SDK notifies the client when device is successfully approved.</param>
        /// <returns>Awaitable boolean result. <c>True</c>True resume login, <c>False</c> cancel.</returns>
        /// <remarks>
        /// Clients to display the list of available device approval channels.
        /// When user picks one clients to check if channel implements <see cref="IDeviceApprovalPushInfo">push interface</see>
        /// then invoke <see cref="IDeviceApprovalPushInfo.InvokeDeviceApprovalPushAction">push action</see>
        /// If channel implements <see cref="ITwoFactorDurationInfo">2FA duration interface</see> clients may show 2FA expiration picker.
        /// </remarks>
        Task<bool> WaitForDeviceApproval(IDeviceApprovalChannelInfo[] channels, CancellationToken token);

        /// <summary>
        /// Two Factor Authentication is required.
        /// </summary>
        /// <param name="channels">List of available 2FA channels.</param>
        /// <param name="token">Cancellation token. Keeper SDK notifies the client passed 2FA.</param>
        /// <returns>Awaitable boolean result. <c>True</c>True resume login, <c>False</c> cancel.</returns>
        /// <remarks>
        /// Clients to display the list of available 2FA channels.
        /// When user picks one clients to check
        /// <list type="number">
        /// <item><description>
        /// if channel implements <see cref="ITwoFactorPushInfo">push interface</see> clients displays an button for each <see cref="ITwoFactorPushInfo.SupportedActions">push action</see>
        /// </description></item>
        /// <item><description>
        /// If channel implements <see cref="ITwoFactorDurationInfo">2FA duration interface</see> clients may show 2FA expiration picker.
        /// </description></item>
        /// <item><description>
        /// If channel implements <see cref="ITwoFactorAppCodeInfo">2FA code interface</see> clients displays 2FA code input.
        /// </description></item>
        /// </list>
        /// When customer enters the code and click Next clients returns the code to <see cref="ITwoFactorAppCodeInfo.InvokeTwoFactorCodeAction">the SDK</see>.
        /// </remarks>
        Task<bool> WaitForTwoFactorCode(ITwoFactorChannelInfo[] channels, CancellationToken token);

        /// <summary>
        /// Master Password is required.
        /// </summary>
        /// <param name="passwordInfo">Enter Password interface</param>
        /// <param name="token">Cancellation token. Keeper SDK notifies the client successfully authorized. Can be ignored.</param>
        /// <returns>Awaitable boolean result. <c>True</c>True resumes login, <c>False</c> cancels.</returns>
        /// <remarks>
        /// Client displays Enter password dialog.
        /// When customer clicks Next client returns the password to <see cref="IPasswordInfo.InvokePasswordActionDelegate">the SDK</see>.
        /// </remarks>
        Task<bool> WaitForUserPassword(IPasswordInfo passwordInfo, CancellationToken token);
    }
}
