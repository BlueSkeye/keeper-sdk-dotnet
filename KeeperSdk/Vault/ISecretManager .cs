using System;
using System.Threading.Tasks;

namespace KeeperSecurity.Vault
{
    public interface ISecretManager
    {
        /// <summary>
        /// Gets Keeper Secret Manager Application Details
        /// </summary>
        /// <param name="applicationUid">Application UID.</param>
        /// <returns>Secret Manager Application Info</returns>
        Task<SecretsManagerApplication> GetSecretManagerApplication(string applicationUid, bool force = true);

        /// <summary>
        /// Creates Secret Manager Application
        /// </summary>
        /// <param name="title">Application Title</param>
        /// <returns>Application Record</returns>
        Task<ApplicationRecord> CreateSecretManagerApplication(string title);

        /// <summary>
        /// Deletes Secret Manager Application
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns>Awaitable Task</returns>
        Task DeleteSecretManagerApplication(string applicationId);


        /// <summary>
        /// Grants Shared Folder or Record Access to Secret Manager Application
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <param name="sharedFolderOrRecordUid">Shared Folder or Record UID</param>
        /// <param name="canEdit">permission to edit</param>
        /// <returns>Secret Manager Application</returns>
        Task<SecretsManagerApplication> ShareToSecretManagerApplication(string applicationId, string sharedFolderOrRecordUid, bool canEdit);

        /// <summary>
        /// Revokes Shared Folder or Record access from Secret Manager Application
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <param name="sharedFolderOrRecordUid">Shared Folder or Record UID</param>
        /// <returns>Secret Manager Application</returns>
        Task<SecretsManagerApplication> UnshareFromSecretManagerApplication(string applicationId, string sharedFolderOrRecordUid);

        /// <summary>
        /// Adds a client/device to Secret Manager Application
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <param name="unlockIp">Optional. If false the first call from the client locks IP. If true no IP locking</param>
        /// <param name="firstAccessExpireInMinutes">Optional. First access duration in minutes. Default: an hour (60). Maximum: a day (1440) </param>
        /// <param name="AccessExpiresInMinutes">Optional. Access Expiration duration in minutes.</param>
        /// <param name="name">Optional. Client/Device name</param>
        /// <returns>Tuple: Client Device, Client Key</returns>
        Task<Tuple<SecretsManagerDevice, string>> AddSecretManagerClient(
            string applicationId, bool? unlockIp = null, int? firstAccessExpireInMinutes = null,
            int? AccessExpiresInMinutes = null, string name = null);

        /// <summary>
        /// Deletes a client/device from Secret Manager Application
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <param name="deviceId">Device ID or Name</param>
        /// <returns>Awaitable Task</returns>
        Task DeleteSecretManagerClient(string applicationId, string deviceId);

    }
}
