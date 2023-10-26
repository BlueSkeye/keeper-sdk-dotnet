using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    [KnownType(typeof(RequestDownload))]
    internal class RequestDownloadResponse : KeeperApiResponse
    {

        [DataMember(Name = "downloads")]
        public RequestDownload[] Downloads;
    }
}
