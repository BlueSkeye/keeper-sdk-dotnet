using System.IO;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines properties of thumbnail upload task.
    /// </summary>
    public interface IThumbnailUploadTask
    {
        /// <summary>
        /// Thumbnail MIME type.
        /// </summary>
        string MimeType { get; }
        /// <summary>
        /// Thumbnail size in pixels.
        /// </summary>
        int Size { get; }
        /// <summary>
        /// Thumbnail read stream.
        /// </summary>
        Stream Stream { get; }
    }
}
