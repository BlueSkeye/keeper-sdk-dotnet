using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class RequestDownload
    {
        [DataMember(Name = "success_status_code")]
        public int SuccessStatusCode;

        [DataMember(Name = "url")]
        public string Url;
    }
}
