using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperSecurity.Authentication.Async
{
    /// <summary>
    /// Defines a method that returns HTTP Web proxy credentials. Optional.
    /// </summary>
    /// <remarks>
    /// Keeper SDK calls this interface if it detects that access to the Internet is protected with HTTP Proxy.
    /// Clients requests HTTP proxy credentials from the user and return them to the library.
    /// </remarks>
    /// <seealso cref="IAuthUI"/>
    public interface IHttpProxyCredentialUi
    {
        /// <summary>
        /// Requests HTTP Proxy credentials.
        /// </summary>
        /// <param name="proxyInfo">HTTP Proxy information</param>
        /// <returns>Awaitable boolean result. <c>True</c>True resume login, <c>False</c> cancel.</returns>
        Task<bool> WaitForHttpProxyCredentials(IHttpProxyInfo proxyInfo);
    }
}
