using System;
using System.Threading.Tasks;

namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Represents device approval step
    /// </summary>
    public class DeviceApprovalStep : AuthStep
    {
        internal DeviceApprovalStep() : base(AuthState.DeviceApproval)
        {
        }

        /// <summary>
        /// Gets or sets default device approval channel
        /// </summary>
        public DeviceApprovalChannel DefaultChannel { get; set; }

        /// <summary>
        /// Gets available device approval channels
        /// </summary>
        public DeviceApprovalChannel[] Channels { get; internal set; }

        internal Func<DeviceApprovalChannel, Task> OnSendPush;

        /// <summary>
        /// Sends push notification to the channel
        /// </summary>
        /// <param name="channel">Device approval channel</param>
        /// <returns>Awaitable task</returns>
        public Task SendPush(DeviceApprovalChannel channel)
        {
            return OnSendPush?.Invoke(channel);
        }

        internal Func<DeviceApprovalChannel, string, Task> OnSendCode;

        /// <summary>
        /// Sends verification code to the channel
        /// </summary>
        /// <param name="channel">Device approval channel</param>
        /// <param name="code">Verification code</param>
        /// <returns>Awaitable task</returns>
        public Task SendCode(DeviceApprovalChannel channel, string code)
        {
            return OnSendCode?.Invoke(channel, code);
        }

        internal Func<Task> OnResume;

        /// <summary>
        /// Resumes login flow
        /// </summary>
        /// <returns>Awaitable task</returns>
        public Task Resume()
        {
            return OnResume?.Invoke();
        }


        internal Action onDispose;

        protected override void Dispose(bool disposing)
        {
            onDispose?.Invoke();
            base.Dispose(disposing);
        }
    }
}
