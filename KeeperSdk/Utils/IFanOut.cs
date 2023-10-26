using System;

namespace KeeperSecurity.Utils
{
    /// <summary>
    ///     Declares fan-out event delivery interface
    /// </summary>
    /// <typeparam name="T">Type of event.</typeparam>
    public interface IFanOut<T> : IDisposable
    {
        /// <summary>
        ///     Gets completion flag.
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        ///     Registers notification callback.
        /// </summary>
        /// <param name="callback"></param>
        void RegisterCallback(NotificationCallback<T> callback);

        /// <summary>
        ///     Removes registered callback.
        /// </summary>
        /// <param name="callback"></param>
        void RemoveCallback(NotificationCallback<T> callback);

        /// <summary>
        ///     Delivers event to subscribers.
        /// </summary>
        /// <param name="message"></param>
        void Push(T message);
    }
}
