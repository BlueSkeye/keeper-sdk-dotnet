using KeeperSecurity.Commands;
using System;
using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines properties and methods of decrypted Vault data.
    /// </summary>
    /// <seealso cref="VaultData"/>
    public interface IVaultData
    {
        /// <summary>
        /// Gets encrypted vault storage.
        /// </summary>
        IKeeperStorage Storage { get; }

        /// <summary>
        /// Gets client key. AES encryption key that encrypts data in the local storage <see cref="Storage"/>
        /// </summary>
        byte[] ClientKey { get; }

        /// <summary>
        /// Gets vault root folder. <c>My Vault</c>
        /// </summary>
        FolderNode RootFolder { get; }

        /// <summary>
        /// Get the list of all folders in the vault. Both user and shared folders.
        /// </summary>
        IEnumerable<FolderNode> Folders { get; }

        /// <summary>
        /// Gets the folder associated with the specified folder UID.
        /// </summary>
        /// <param name="folderUid">Folder UID</param>
        /// <param name="node">When this method returns <c>true</c>, contains requested folder; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> in the vault contains a folder with specified UID; otherwise, <c>false</c></returns>
        bool TryGetFolder(string folderUid, out FolderNode node);

        /// <summary>
        /// Gets the number of all records in the vault.
        /// </summary>
        int RecordCount { get; }

        /// <summary>
        /// Get the list of all records in the vault.
        /// </summary>
        IEnumerable<KeeperRecord> KeeperRecords { get; }
        /// <summary>
        /// Gets the legacy record associated with the specified record UID.
        /// </summary>
        /// <param name="recordUid">Record UID.</param>
        /// <param name="record">When this method returns <c>true</c>, contains requested record; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> in the vault contains a record with specified UID; otherwise, <c>false</c></returns>
        bool TryGetKeeperRecord(string recordUid, out KeeperRecord record);

        /// <summary>
        /// Get the list of all legacy records in the vault.
        /// </summary>
        [Obsolete("Use KeeperRecords")]
        IEnumerable<PasswordRecord> Records { get; }
        /// <summary>
        /// Gets the legacy record associated with the specified record UID.
        /// </summary>
        /// <param name="recordUid">Record UID.</param>
        /// <param name="record">When this method returns <c>true</c>, contains requested record; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> in the vault contains a record with specified UID; otherwise, <c>false</c></returns>
        [Obsolete("Use TryGetKeeperRecord")]
        bool TryGetRecord(string recordUid, out PasswordRecord record);

        /// <summary>
        /// Gets  number of all shared folders in the vault.
        /// </summary>
        int SharedFolderCount { get; }
        /// <summary>
        /// Get the list of all shared folders in the vault.
        /// </summary>
        IEnumerable<SharedFolder> SharedFolders { get; }
        /// <summary>
        /// Gets shared folder associated with a specified record UID.
        /// </summary>
        /// <param name="sharedFolderUid">Shared Folder UID</param>
        /// <param name="sharedFolder">When this method returns <c>true</c>, contains requested shared folder; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> in the vault contains a shared folder with specified UID; otherwise, <c>false</c>.</returns>
        bool TryGetSharedFolder(string sharedFolderUid, out SharedFolder sharedFolder);

        /// <summary>
        /// Gets the number of all teams user is member of.
        /// </summary>
        int TeamCount { get; }
        /// <summary>
        /// Get list of all teams user is member of.
        /// </summary>
        IEnumerable<Team> Teams { get; }
        /// <summary>
        /// Gets a team associated with a specified team UID.
        /// </summary>
        /// <param name="teamUid">Team UID.</param>
        /// <param name="team">When this method returns <c>true</c>, contains requested team; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> in the vault contains a team with specified UID; otherwise, <c>false</c>.</returns>
        bool TryGetTeam(string teamUid, out Team team);

        /// <summary>
        /// Loads non shared (or per user) data associated with the record.
        /// </summary>
        /// <typeparam name="T">App specific per-user data type</typeparam>
        /// <param name="recordUid">Record UID</param>
        /// <returns>Non shared data associated with the record</returns>
        T LoadNonSharedData<T>(string recordUid) where T : RecordNonSharedData, new();

        /// <summary>
        /// Gets list of all registered record types.
        /// </summary>
        IEnumerable<RecordType> RecordTypes { get; }
        /// <summary>
        /// Gets record type meta data associated with the record type name.
        /// </summary>
        /// <param name="name">Record type name.</param>
        /// <param name="recordType">When this method returns <c>true</c>, contains requested record type; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if record type exists; otherwise, <c>false</c>.</returns>
        bool TryGetRecordTypeByName(string name, out RecordType recordType);

        /// <summary>
        /// Gets number of all Keeper Secret Manager Applications.
        /// </summary>
        int ApplicationCount { get; }
        /// <summary>
        /// Gets list of all Keeper Secret Manager Applications.
        /// </summary>
        IEnumerable<ApplicationRecord> KeeperApplications { get; }
        /// <summary>
        /// Gets a KSM application associated with a specified team UID.
        /// </summary>
        /// <param name="applicationUid">Team UID.</param>
        /// <param name="application">When this method returns <c>true</c>, contains requested team; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> in the vault contains a application with specified UID; otherwise, <c>false</c>.</returns>
        bool TryGetKeeperApplication(string applicationUid, out ApplicationRecord application);
    }
}
