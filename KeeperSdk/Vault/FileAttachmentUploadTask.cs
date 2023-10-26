using System;
using System.Diagnostics;
using System.IO;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Creates a file attachment upload task.
    /// </summary>
    public class FileAttachmentUploadTask : AttachmentUploadTask, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FileAttachmentUploadTask"/> class.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="thumbnail">Thumbnail upload task. Optional.</param>
        public FileAttachmentUploadTask(string fileName, IThumbnailUploadTask thumbnail = null)
            : base(null, thumbnail)
        {
            if (File.Exists(fileName))
            {
                Name = Path.GetFileName(fileName);
                Title = Name;
                try
                {
                    MimeType = MimeTypes.MimeTypeMap.GetMimeType(Path.GetExtension(fileName));
                }
                catch
                {
                    // ignored
                }

                Stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            else
            {
                Trace.TraceError("FileAttachmentUploadTask: fileName: \"{0}\" not found.", fileName);
            }
        }

        public void Dispose()
        {
            Stream?.Dispose();
        }
    }
}
