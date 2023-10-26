using System.IO;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines properties of file upload task.
    /// </summary>
    public interface IAttachmentUploadTask
    {
        /// <summary>
        /// Attachment name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Attachment title.
        /// </summary>
        string Title { get; }
        /// <summary>
        /// Attachment MIME type.
        /// </summary>
        string MimeType { get; }
        /// <summary>
        /// Attachment read stream.
        /// </summary>
        Stream Stream { get; }

        /// <summary>
        /// Thumbnail upload task. Optional.
        /// </summary>
        IThumbnailUploadTask Thumbnail { get; }
    }
}
