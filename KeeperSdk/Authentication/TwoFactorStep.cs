using System;
using System.Threading.Tasks;

namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Represents Two Factor Authentication step
    /// </summary>
    public class TwoFactorStep : AuthStep
    {
        internal TwoFactorStep() : base(AuthState.TwoFactor)
        {
        }

        /// <summary>
        /// Gets or sets default two factor authentication channel
        /// </summary>
        public TwoFactorChannel DefaultChannel { get; set; }

        /// <summary>
        /// Gets available two factor authentication channels
        /// </summary>
        public TwoFactorChannel[] Channels { get; internal set; }

        /// <summary>
        /// Gets / sets two factor authentication duration / expiration
        /// </summary>
        public TwoFactorDuration Duration { get; set; }

        internal Func<TwoFactorChannel, TwoFactorPushAction[]> OnGetChannelPushActions;

        /// <summary>
        /// Gets available push actions for the channel
        /// </summary>
        /// <param name="channel">Two factor authentication channel</param>
        /// <returns>List of available push actions</returns>
        public TwoFactorPushAction[] GetChannelPushActions(TwoFactorChannel channel)
        {
            return OnGetChannelPushActions != null ? OnGetChannelPushActions(channel) : new TwoFactorPushAction[] { };
        }

        internal Func<TwoFactorChannel, bool> OnIsCodeChannel;

        /// <summary>
        /// Gets flag if channel accepts verification codes
        /// </summary>
        /// <param name="channel">Two factor authentication channel</param>
        /// <returns><c>True</c> if the channel supports verification codes</returns>
        public bool IsCodeChannel(TwoFactorChannel channel)
        {
            return OnIsCodeChannel?.Invoke(channel) ?? false;
        }

        internal Func<TwoFactorChannel, string> OnGetPhoneNumber;

        /// <summary>
        /// Gets phone number for the channel
        /// </summary>
        /// <param name="channel">Two factor authentication channel</param>
        /// <returns>Phone number registered to the channel.</returns>
        public string GetPhoneNumber(TwoFactorChannel channel)
        {
            return OnGetPhoneNumber?.Invoke(channel);
        }

        internal Func<TwoFactorPushAction, Task> OnSendPush;

        /// <summary>
        /// Sends push action to the channel
        /// </summary>
        /// <param name="action">Push action</param>
        /// <returns>Awaitable task</returns>
        public Task SendPush(TwoFactorPushAction action)
        {
            return OnSendPush?.Invoke(action);
        }

        internal Func<TwoFactorChannel, string, Task> OnSendCode;

        /// <summary>
        /// Sends verification code
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="code"></param>
        /// <returns>Awaitable task</returns>
        public Task SendCode(TwoFactorChannel channel, string code)
        {
            return OnSendCode?.Invoke(channel, code);
        }

        internal Func<Task> OnResume;

        /// <summary>
        /// Sends verification code
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="code"></param>
        /// <returns>Awaitable task</returns>
        public Task Resume()
        {
            return OnResume?.Invoke();
        }

        internal Action OnDispose;

        protected override void Dispose(bool disposing)
        {
            OnDispose?.Invoke();
            base.Dispose(disposing);
        }
    }
}
