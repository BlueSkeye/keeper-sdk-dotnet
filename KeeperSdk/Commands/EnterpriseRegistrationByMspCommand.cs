using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class EnterpriseRegistrationByMspCommand : EnterpriseManagedCompanyByMspCommand
    {
        public EnterpriseRegistrationByMspCommand() : base("enterprise_registration_by_msp")
        {
        }

        [DataMember(Name = "role_data")]
        public string RoleData { get; set; }

        [DataMember(Name = "root_node")]
        public string RootNode { get; set; }

        [DataMember(Name = "encrypted_tree_key")]
        public string EncryptedTreeKey { get; set; }
    }
}
