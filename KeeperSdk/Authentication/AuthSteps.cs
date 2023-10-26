using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests")]

namespace KeeperSecurity.Authentication.Sync
{







    /// <summary>
    /// Represents SSO Approval step
    /// </summary>
    public class SsoDataKeyStep : AuthStep
    {
        internal SsoDataKeyStep() : base(AuthState.SsoDataKey)
        {
        }

        /// <summary>
        /// Gets available SSO approval channels
        /// </summary>
        public DataKeyShareChannel[] Channels { get; internal set; }

        internal Func<DataKeyShareChannel, Task> OnRequestDataKey { get; set; }

        /// <summary>
        /// Requests SSO Approval
        /// </summary>
        /// <param name="channel">SSO approval channel</param>
        public Task RequestDataKey(DataKeyShareChannel channel)
        {
            return OnRequestDataKey?.Invoke(channel);
        }

        internal Func<Task> onResume;
        /// <summary>
        /// Resumes login flow
        /// </summary>
        public Task Resume()
        {
            return onResume?.Invoke();
        }
    }

    /// <summary>
    /// Represents Connected step. Final step. Successfully connected to Keeper.
    /// </summary>
    public class ConnectedStep : AuthStep
    {
        internal ConnectedStep() : base(AuthState.Connected)
        {
        }
    }

    /// <summary>
    /// Represents Error step. Final step. Failed to connect to Keeper.
    /// </summary>
    public class ErrorStep : AuthStep
    {
        internal ErrorStep(string code, string message) : base(AuthState.Error)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Gets error code
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Get error message
        /// </summary>
        public string Message { get; }
    }

    /// <summary>
    /// Represents Restricted Connection step. Final step. The connection is limited only to certain commands.
    /// </summary>
    public class RestrictedConnectionStep : AuthStep
    {
        public RestrictedConnectionStep() : base(AuthState.Restricted)
        {
        }
    }
}
