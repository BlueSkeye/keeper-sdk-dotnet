using System;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines property for file attachment
    /// </summary>
    public interface IAttachment
    {
        /// <summary>
        /// Attachment ID.
        /// </summary>
        string Id { get; }
        /// <summary>
        /// Attachment name.
        /// </summary>
        /// <remarks>Usually it is an original file name.</remarks>
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
        /// Attachment size in bytes.
        /// </summary>
        long Size { get; }
        /// <summary>
        /// Last time modified.
        /// </summary>
        DateTimeOffset LastModified { get; }
        /// <summary>
        /// Attachment encryption key.
        /// </summary>
        byte[] AttachmentKey { get; }
    }
}
