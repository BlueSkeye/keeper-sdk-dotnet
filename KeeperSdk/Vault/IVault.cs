using KeeperSecurity.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines methods for modifying the vault records and folders. 
    /// </summary>
    /// <seealso cref="VaultOnline"/>
    public interface IVault : IVaultData
    {
        /// <summary>
        /// Gets Vault user interaction interface.
        /// </summary>
        IVaultUi VaultUi { get; }

        /// <summary>
        /// Gets or Sets automatic sync down flag.
        /// </summary>
        bool AutoSync { get; set; }

        /// <summary>
        /// Records "open_record" audit event for enterprise accounts
        /// </summary>
        /// <param name="recordUid"></param>
        void AuditLogRecordOpen(string recordUid);

        /// <summary>
        /// Records "copy_password" audit event for enterprise accounts
        /// </summary>
        /// <param name="recordUid"></param>
        void AuditLogRecordCopyPassword(string recordUid);

        /// <summary>
        /// Creates a password record.
        /// </summary>
        /// <param name="record">Keeper Record.</param>
        /// <param name="folderUid">Folder UID where the record to be created. Optional.</param>
        /// <returns>A task returning created password record.</returns>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        Task<KeeperRecord> CreateRecord(KeeperRecord record, string folderUid = null);

        /// <summary>
        /// Modifies a password record.
        /// </summary>
        /// <param name="record">Keeper Record.</param>
        /// <param name="skipExtra">Do not update file attachment information on the record.</param>
        /// <returns>A task returning created password record.</returns>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        Task<KeeperRecord> UpdateRecord(KeeperRecord record, bool skipExtra = true);

        /// <summary>
        /// Deletes password records.
        /// </summary>
        /// <param name="records">an array of record paths.</param>
        /// <returns>Awaitable task.</returns>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        Task DeleteRecords(RecordPath[] records);

        /// <summary>
        /// Moves records to a folder.
        /// </summary>
        /// <param name="records">an array of record paths.</param>
        /// <param name="dstFolderUid">Destination folder UID.</param>
        /// <param name="link"><c>true</c>creates a link. The source record in not deleted; otherwise record will be removed from the source.</param>
        /// <returns>Awaitable task.</returns>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        Task MoveRecords(RecordPath[] records, string dstFolderUid, bool link = false);

        /// <summary>
        /// Stores non shared (or per user) data associated with the record.
        /// </summary>
        /// <typeparam name="T">App specific per-user data type</typeparam>
        /// <param name="recordUid">Record UID</param>
        /// <param name="nonSharedData">Non shared data</param>
        /// <returns>Awaitable task.</returns>
        /// <exception cref="Authentication.KeeperApiException">Keeper API error</exception>
        Task StoreNonSharedData<T>(string recordUid, T nonSharedData) where T : RecordNonSharedData, new();

        /// <summary>
        /// Creates a folder.
        /// </summary>
        /// <param name="name">Folder Name.</param>
        /// <param name="parentFolderUid">Parent Folder UID.</param>
        /// <param name="sharedFolderOptions">Shared Folder creation options. Optional.</param>
        /// <returns>A task returning created folder.</returns>
        /// <remarks>Pass <see cref="sharedFolderOptions"/> parameter to create a Shared Folder.</remarks>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        /// <seealso cref="SharedFolderOptions"/>
        Task<FolderNode> CreateFolder(string name, string parentFolderUid = null, SharedFolderOptions sharedFolderOptions = null);
        /// <summary>
        /// Renames a folder.
        /// </summary>
        /// <param name="folderUid">Folder UID.</param>
        /// <param name="newName">New folder name.</param>
        /// <returns>A task returning renamed folder.</returns>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        Task<FolderNode> RenameFolder(string folderUid, string newName);
        /// <summary>
        /// Renames a folder.
        /// </summary>
        /// <param name="folderUid">Folder UID.</param>
        /// <param name="newName">New folder name.</param>
        /// <param name="sharedFolderOptions">Shared Folder creation options. Optional.</param>
        /// <returns>A task returning renamed folder.</returns>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        Task<FolderNode> UpdateFolder(string folderUid, string newName, SharedFolderOptions sharedFolderOptions = null);
        
        /// <summary>
        /// Moves a folder to the another folder.
        /// </summary>
        /// <param name="srcFolderUid">Source Folder UID.</param>
        /// <param name="dstFolderUid">Destination Folder UID.</param>
        /// <param name="link"><c>true</c>creates a link. The source folder in not deleted; otherwise source folder will be removed.</param>
        /// <returns>Awaitable task.</returns>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        Task MoveFolder(string srcFolderUid, string dstFolderUid, bool link = false);
        /// <summary>
        /// Delete folder.
        /// </summary>
        /// <param name="folderUid">Folder UID.</param>
        /// <returns>Awaitable task.</returns>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        Task DeleteFolder(string folderUid);

        /// <summary>
        /// Retrieves all enterprise team descriptions.
        /// </summary>
        /// <returns>A list of all enterprise teams. (awaitable)</returns>
        Task<IEnumerable<TeamInfo>> GetTeamsForShare();

        /// <summary>
        /// Retrieves all known users for sharing
        /// </summary>
        /// <returns></returns>
        Task<ShareWithUsers> GetUsersForShare();

        /// <summary>
        /// Gets user public keys.
        /// </summary>
        /// <param name="username"></param>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        /// <exception cref="NoActiveShareWithUserException"/>
        /// <returns>Awaitable task returning RSA and ECC public keys</returns>
        Task<Tuple<byte[], byte[]>> GetUserPublicKeys(string username);

        /// <summary>
        /// Sends share invitation request to the user.
        /// </summary>
        /// <param name="username">User email</param>
        /// <returns>Awaitable task</returns>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        Task SendShareInvitationRequest(string username);

        /// <summary>
        /// Retrieves record sharing information. 
        /// </summary>
        /// <param name="recordUids">List of record UIDs</param>
        /// <returns>Awaitable task returning record share details</returns>
        Task<IEnumerable<RecordSharePermissions>> GetSharesForRecords(IEnumerable<string> recordUids);

        /// <summary>
        /// Cancels all shares with a user.
        /// </summary>
        /// <param name="username">User account email.</param>
        /// <returns>Awaitable task.</returns>
        /// <exception cref="Authentication.KeeperApiException"></exception>
        Task CancelSharesWithUser(string username);

        /// <summary>
        /// Shares a record with a user
        /// </summary>
        /// <param name="recordUid">Record UID.</param>
        /// <param name="username">User account email</param>
        /// <param name="canReshare">Can record be re-shared</param>
        /// <param name="canEdit">Can record be modified</param>
        /// <exception cref="NoActiveShareWithUserException"/>
        /// <returns>Awaitable task.</returns>
        Task ShareRecordWithUser(string recordUid, string username, bool? canReshare, bool? canEdit);

        /// <summary>
        /// Transfers a record to user
        /// </summary>
        /// <param name="recordUid">Record UID.</param>
        /// <param name="username">User account email</param>
        /// <returns>Awaitable task.</returns>
        Task TransferRecordToUser(string recordUid, string username);

        /// <summary>
        /// Removes a record share from a user
        /// </summary>
        /// <param name="recordUid">Record UID.</param>
        /// <param name="username">User account email</param>
        /// <returns>Awaitable task.</returns>
        Task RevokeShareFromUser(string recordUid, string username);
    }
}
