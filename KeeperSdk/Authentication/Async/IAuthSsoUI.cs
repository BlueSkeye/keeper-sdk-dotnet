using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeeperSecurity.Authentication.Async
{
    /// <summary>
    /// Defines the methods required completing SSO Login. Optional.
    /// </summary>
    /// <remarks>If client supports SSO Login this interface needs to be implemented
    /// along with <see cref="IAuthUI">Auth UI</see>
    /// </remarks>
    /// <seealso cref="IAuthUI"/>
    /// <remarks>
    /// Client implements this interface to support SSO login. This interface will be called in response
    /// of <see cref="IAuth.Login"/> if username is SSO user or <see cref="IAuth.LoginSso"/>
    /// </remarks>
    public interface IAuthSsoUI : ISsoLogoutCallback
    {
        /// <summary>
        /// SSO Login is required.
        /// </summary>
        /// <param name="actionInfo"></param>
        /// <param name="token">Cancellation token. Keeper SDK notifies the client successfully logged in with SSO.</param>
        /// <returns>Awaitable boolean result. <c>True</c>True resume login, <c>False</c> cancel.</returns>
        /// <remarks>
        /// When this method is called client opens embedded web browser and navigates to URL specified in
        /// <see cref="ISsoTokenActionInfo.SsoLoginUrl">actionInfo.SsoLoginUrl</see>
        /// then monitors embedded web browser navigation.
        /// When it finds the page that contains <c>window.token</c> object it passes this object to
        /// <see cref="ISsoTokenActionInfo.InvokeSsoTokenAction">actionInfo.InvokeSsoTokenAction</see>
        /// </remarks>
        Task<bool> WaitForSsoToken(ISsoTokenActionInfo actionInfo, CancellationToken token);

        /// <summary>
        /// Data Key needs to be shared. 
        /// </summary>
        /// <param name="channels">List of available data key sharing channels.</param>
        /// <param name="token">Cancellation token. Keeper SDK notifies the client that data key is shared.</param>
        /// <returns>Awaitable boolean result. <c>True</c>True resume login, <c>False</c> cancel.</returns>
        /// <remarks>
        /// Cloud SSO login may require user data key to be shared if the device is used for the first time.
        /// Client displays the list of available data key sharing channels.
        /// When user picks a channel, client invokes channel's action <see cref="IDataKeyChannelInfo.InvokeGetDataKeyAction">channels.InvokeGetDataKeyAction</see>
        /// </remarks>
        Task<bool> WaitForDataKey(IDataKeyChannelInfo[] channels, CancellationToken token);
    }
}
