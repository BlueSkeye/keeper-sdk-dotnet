using System;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents a Keeper File Record.
    /// </summary>
    public class FileRecord : KeeperRecord, IAttachment
    {
        /// <summary>
        /// File Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File MIME type.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// File size in bytes.
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// File size in bytes.
        /// </summary>
        public long ThumbnailSize { get; set; }

        /// <summary>
        /// Last time modified.
        /// </summary>
        public DateTimeOffset LastModified { get; set; }

        /// <summary>
        /// On storage file size in bytes.
        /// </summary>
        public long? StorageFileSize { get; internal set; }

        /// <summary>
        /// On storage thumbnail size in bytes.
        /// </summary>
        public long? StorageThumbnailSize { get; internal set; }

        string IAttachment.Id => Uid;
        long IAttachment.Size => FileSize;
        byte[] IAttachment.AttachmentKey => RecordKey;
    }
}
