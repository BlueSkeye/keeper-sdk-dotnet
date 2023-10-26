using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class EnterpriseManagedCompanyByMspCommand : AuthenticatedCommand
    {
        public EnterpriseManagedCompanyByMspCommand(string command) : base(command)
        {
        }

        [DataMember(Name = "enterprise_name")]
        public string EnterpriseName { get; set; }

        [DataMember(Name = "node_id", EmitDefaultValue = false)]
        public long? NodeId { get; set; }

        [DataMember(Name = "product_id")]
        public string ProductId { get; set; }

        [DataMember(Name = "seats")]
        public int Seats { get; set; }

        [DataMember(Name = "file_plan_type", EmitDefaultValue = false)]
        public string FilePlanType { get; set; }

        [DataMember(Name = "add_ons", EmitDefaultValue = false)]
        public MspAddon[] AddOns { get; set; }
    }
}
