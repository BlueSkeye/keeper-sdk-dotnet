
namespace KeeperSecurity.Utils
{
    /// <summary>
    ///     Notification callback delegate.
    /// </summary>
    /// <typeparam name="T">Type of event</typeparam>
    /// <param name="evt">Notification event.</param>
    /// <returns><c>true</c> to remove callback. <c>false</c> keep receiving events.</returns>
    /// <seealso cref="IFanOut&lt;T&gt;" />
    public delegate bool NotificationCallback<in T>(T evt);
}
