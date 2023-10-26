using System;

namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Represents base Keeper authentication step
    /// </summary>
    /// <seealso cref="LoginStep"/>
    /// <seealso cref="HttpProxyStep"/>
    /// <seealso cref="DeviceApprovalStep"/>
    /// <seealso cref="TwoFactorStep"/>
    /// <seealso cref="PasswordStep"/>
    /// <seealso cref="SsoTokenStep"/>
    /// <seealso cref="SsoDataKeyStep"/>
    /// <seealso cref="ConnectedStep"/>
    /// <seealso cref="ErrorStep"/>
    public abstract class AuthStep : IDisposable
    {
        protected AuthStep(AuthState state)
        {
            State = state;
        }

        /// <summary>
        /// Gets Keeper login state
        /// </summary>
        public AuthState State { get; }

        protected virtual void Dispose(bool disposing)
        {
        }
        /// <exclude/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
