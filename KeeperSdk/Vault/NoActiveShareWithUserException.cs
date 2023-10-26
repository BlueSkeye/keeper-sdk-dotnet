
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents an exception that occurs when current user requests other user's public for the first time.
    /// </summary>
    public class NoActiveShareWithUserException : Authentication.KeeperApiException
    {
        public NoActiveShareWithUserException(string username, string code, string message) : base(code, message)
        {
            Username = username;
        }

        /// <summary>
        /// Gets user email to send share invite
        /// </summary>
        public string Username { get; }
    }
}
