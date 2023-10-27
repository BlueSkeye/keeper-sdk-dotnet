using KeeperSecurity.Utils;
using System;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents attachment file.
    /// </summary>
    public class AttachmentFile : IAttachment
    {
        /// <summary>
        /// Attachment ID.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Attachment encryption key.
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Attachment name.
        /// </summary>
        /// <remarks>Usually it is an original file name.</remarks>
        public string Name { get; set; }
        /// <summary>
        /// Attachment title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Attachment MIME type.
        /// </summary>
        public string MimeType { get; set; }
        /// <summary>
        /// Attachment size in bytes.
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// Last time modified.
        /// </summary>
        public DateTimeOffset LastModified { get; set; }
        /// <summary>
        /// A list of thumbnails.
        /// </summary>
        public AttachmentFileThumb[] Thumbnails { get; internal set; }

        byte[] IAttachment.AttachmentKey => string.IsNullOrEmpty(Key) ? null : Key.Base64UrlDecode();
    }
}
