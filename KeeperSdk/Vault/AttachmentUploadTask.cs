using System.IO;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Creates an attachment upload task.
    /// </summary>
    public class AttachmentUploadTask : IAttachmentUploadTask
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AttachmentUploadTask"/> class.
        /// </summary>
        /// <param name="attachmentStream"></param>
        /// <param name="thumbnail"></param>
        public AttachmentUploadTask(Stream attachmentStream, IThumbnailUploadTask thumbnail = null)
        {
            Thumbnail = thumbnail;
            Stream = attachmentStream;
        }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Title { get; set; }

        /// <inheritdoc/>
        public string MimeType { get; set; }

        /// <inheritdoc/>
        public Stream Stream { get; protected set; }

        /// <inheritdoc/>
        public IThumbnailUploadTask Thumbnail { get; protected set; }
    }
}
