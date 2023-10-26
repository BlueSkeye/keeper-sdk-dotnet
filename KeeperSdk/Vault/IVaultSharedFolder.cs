using System.Threading.Tasks;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines methods to manipulate Shared Folders.
    /// </summary>
    /// <seealso cref="VaultOnline"/>
    public interface IVaultSharedFolder
    {
        /// <summary>
        /// Adds (if needed) user or team to the shared folder and set user access permissions.
        /// </summary>
        /// <param name="sharedFolderUid">Shared Folder UID.</param>
        /// <param name="userId">User email or Team UID.</param>
        /// <param name="userType">Type of <see cref="userId"/> parameter.</param>
        /// <param name="options">Shared Folder User Permissions.</param>
        /// <returns>Awaitable task.</returns>
        /// <remarks>
        /// If <seealso cref="options"/> parameter is <c>null</c> then user gets default user permissions when added./>
        /// </remarks>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        /// <exception cref="NoActiveShareWithUserException" />
        /// <seealso cref="SharedFolderUserOptions"/>
        Task PutUserToSharedFolder(string sharedFolderUid, string userId, UserType userType, ISharedFolderUserOptions options = null);
        /// <summary>
        /// Removes user or team from shared folder.
        /// </summary>
        /// <param name="sharedFolderUid">Shared Folder UID.</param>
        /// <param name="userId">User email or Team UID.</param>
        /// <param name="userType">Type of <see cref="userId"/> parameter.</param>
        /// <returns>Awaitable task.</returns>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        Task RemoveUserFromSharedFolder(string sharedFolderUid, string userId, UserType userType);
        /// <summary>
        /// Changes record permissions in shared folder.
        /// </summary>
        /// <param name="sharedFolderUid">Shared Folder UID.</param>
        /// <param name="recordUid">Record UID.</param>
        /// <param name="options">Record permissions.</param>
        /// <returns></returns>
        /// <remarks>
        /// This method does not add a record to shared folder.
        /// Use <see cref="IVault.CreateRecord"/> or <see cref="IVault.MoveRecords"/>.
        /// </remarks>
        /// <seealso cref="SharedFolderRecordOptions"/>
        Task ChangeRecordInSharedFolder(string sharedFolderUid, string recordUid, ISharedFolderRecordOptions options);
    }
}
