using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class RequestUploadResponse : KeeperApiResponse
    {
        [DataMember(Name = "file_uploads")]
        public UploadParameters[] FileUploads;

        [DataMember(Name = "thumbnail_uploads")]
        public UploadParameters[] ThumbnailUploads;
    }
}
