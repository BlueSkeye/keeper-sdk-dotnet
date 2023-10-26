
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents a thumbnail of attachment.
    /// </summary>
    /// <remarks>It usually is used for large image thumbnails.</remarks>
    public class AttachmentFileThumb
    {
        /// <summary>
        /// Thumbnail ID.
        /// </summary>
        public string Id { get; internal set; }
        /// <summary>
        /// Thumbnail MIME type.
        /// </summary>
        public string Type { get; internal set; }
        /// <summary>
        /// Thumbnail size. pixels.
        /// </summary>
        public int Size { get; internal set; }
    }
}
