using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class EnterpriseUserAddResponse : KeeperApiResponse
    {
        [DataMember(Name = "verification_code")]
        public string VerificationCode { get; set; }
    }
}
