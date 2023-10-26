
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines properties for shared folder user permissions.
    /// </summary>
    public interface ISharedFolderPermission : IUidLink
    {
        /// <summary>
        /// Shared folder UID.
        /// </summary>
        string SharedFolderUid { get; }
        /// <summary>
        /// User email or Team UID.
        /// </summary>
        string UserId { get; }
        /// <summary>
        /// User type.
        /// </summary>
        /// <seealso cref="Vault.UserType"/>
        int UserType { get; }
        /// <summary>
        /// Can manage records?
        /// </summary>
        bool ManageRecords { get; }
        /// <summary>
        /// Can manage users?
        /// </summary>
        bool ManageUsers { get; }
    }
}
