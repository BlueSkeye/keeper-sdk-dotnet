using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class UploadParameters
    {
        [DataMember(Name = "url")]
        public string Url;

        [DataMember(Name = "max_size")]
        public long MaxSize;

        [DataMember(Name = "success_status_code")]
        public int SuccessStatusCode;

        [DataMember(Name = "file_id")]
        public string FileId;

        [DataMember(Name = "file_parameter")]
        public string FileParameter;

        [DataMember(Name = "parameters")]
        public IDictionary<string, object> Parameters;

    }
}
