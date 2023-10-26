using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class RequestUploadCommand : AuthenticatedCommand
    {
        public RequestUploadCommand() : base("request_upload")
        {
        }

        [DataMember(Name = "file_count")]
        public int FileCount = 0;

        [DataMember(Name = "thumbnail_count")]
        public int ThumbnailCount = 0;
    }
}
