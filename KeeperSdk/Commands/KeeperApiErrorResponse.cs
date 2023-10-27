using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class KeeperApiErrorResponse
    {
        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "additional_info")]
        public string AdditionalInfo { get; set; }

        [DataMember(Name = "location")]
        public string Location { get; set; }

        [DataMember(Name = "key_id")]
        public int KeyId { get; set; }

        [DataMember(Name = "region_host")]
        public string RegionHost { get; set; }
    }
}
