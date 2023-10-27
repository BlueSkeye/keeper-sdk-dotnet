using Google.Protobuf;
using KeeperSecurity.Commands;
using System;
using System.Threading.Tasks;

namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Defines properties and methods of connected Keeper authentication object.
    /// </summary>
    public interface IAuthentication : IAuthEndpoint
    {
        /// <summary>
        /// Gets authentication context
        /// </summary>
        IAuthContext AuthContext { get; }

        /// <summary>
        /// Executes Keeper JSON command.
        /// </summary>
        /// <param name="command">Keeper JSON command.</param>
        /// <param name="responseType">Type of response.</param>
        /// <param name="throwOnError">throws exception on error.</param>
        /// <returns>Task returning JSON response.</returns>
        /// <seealso cref="Auth.ExecuteAuthCommand(AuthenticatedCommand,System.Type)"/>
        Task<KeeperApiResponse> ExecuteAuthCommand(AuthenticatedCommand command, Type responseType, bool throwOnError);

        /// <summary>
        /// Executes Keeper Protobuf request.
        /// </summary>
        /// <param name="endpoint">Request endpoint.</param>
        /// <param name="request">Protobuf request.</param>
        /// <param name="responseType">Expected response type</param>
        /// <returns>Task returning Protobuf response.</returns>
        /// <seealso cref="Auth.ExecuteAuthRest"/>
        Task<IMessage> ExecuteAuthRest(string endpoint, IMessage request, Type responseType = null);

        /// <summary>
        /// Logout from Keeper server.
        /// </summary>
        /// <returns>Awaitable Task</returns>
        Task Logout();

        /// <exclude/>
        Task AuditEventLogging(string eventType, AuditEventInput input = null);
    }
}
