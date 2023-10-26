using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class EnterpriseUpdateByMspCommand : EnterpriseManagedCompanyByMspCommand
    {
        public EnterpriseUpdateByMspCommand() : base("enterprise_update_by_msp")
        {
        }

        [DataMember(Name = "enterprise_id")]
        public int EnterpriseId { get; set; }

        [DataMember(Name = "notification", EmitDefaultValue = false)]
        public int Notification { get; set; }

        [DataMember(Name = "price", EmitDefaultValue = false)]
        public string Price { get; set; }
    }
}
