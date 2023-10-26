using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class EnterpriseUserAddCommand : EnterpriseUserCommand
    {
        public EnterpriseUserAddCommand() : base("enterprise_user_add")
        {
        }

        [DataMember(Name = "enterprise_user_username")]
        public string EnterpriseUserUsername { get; set; }

        [DataMember(Name = "node_id")]
        public long NodeId { get; set; }

        [DataMember(Name = "encrypted_data", EmitDefaultValue = false)]
        public string EncryptedData { get; set; }

        [DataMember(Name = "full_name", EmitDefaultValue = false)]
        public string FullName { get; set; }

        [DataMember(Name = "job_title", EmitDefaultValue = false)]
        public string JobTitle { get; set; }

        [DataMember(Name = "suppress_email_invite", EmitDefaultValue = false)]
        public bool? SuppressEmailInvite { get; set; }
    }
}
