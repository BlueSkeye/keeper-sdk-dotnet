using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines methods to manipulate file attachments.
    /// </summary>
    public interface IVaultFileAttachment
    {
        /// <summary>
        /// Returns Record attachments
        /// </summary>
        /// <param name="record">Keeper record</param>
        /// <returns>List od attachments</returns>
        IEnumerable<IAttachment> RecordAttachments(KeeperRecord record);

        /// <summary>
        /// Downloads and decrypts file attachment.
        /// </summary>
        /// <param name="record">Keeper record.</param>
        /// <param name="attachment">Attachment name, title, or ID.</param>
        /// <param name="destination">Writable stream.</param>
        /// <returns>Awaitable task.</returns>
        Task DownloadAttachment(KeeperRecord record, string attachment, Stream destination);

        /// <summary>
        /// Encrypts and uploads file attachment.
        /// </summary>
        /// <param name="record">Keeper record</param>
        /// <param name="uploadTask">Upload task</param>
        /// <returns>Awaitable task.</returns>
        Task UploadAttachment(KeeperRecord record, IAttachmentUploadTask uploadTask);

        /// <summary>
        /// Deletes file attachment.
        /// </summary>
        /// <param name="record">Keeper record.</param>
        /// <param name="attachmentId">Attachment ID</param>
        /// <returns>Awaitable task.</returns>
        Task<bool> DeleteAttachment(KeeperRecord record, string attachmentId);
    }
}
