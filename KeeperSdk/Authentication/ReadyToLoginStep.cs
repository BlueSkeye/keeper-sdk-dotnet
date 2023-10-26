
namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Represents initial Login step
    /// </summary>
    public class ReadyToLoginStep : AuthStep
    {
        internal ReadyToLoginStep() : base(AuthState.NotConnected)
        {
        }
    }
}
