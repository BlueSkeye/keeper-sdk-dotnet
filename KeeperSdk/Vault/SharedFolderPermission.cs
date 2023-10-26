
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents shared folder user permissions.
    /// </summary>
    public class SharedFolderPermission
    {
        /// <summary>
        /// User email or team UID.
        /// </summary>
        public string UserId { get; internal set; }
        /// <summary>
        /// The type of <see cref="UserId"/> property.
        /// </summary>
        public UserType UserType { get; internal set; }
        /// <summary>
        /// Can Manage Records?
        /// </summary>
        public bool ManageRecords { get; internal set; }
        /// <summary>
        /// Can Manage Users?
        /// </summary>
        public bool ManageUsers { get; internal set; }
    }
}
