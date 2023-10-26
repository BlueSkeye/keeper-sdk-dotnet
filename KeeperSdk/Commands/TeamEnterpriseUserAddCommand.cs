using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class TeamEnterpriseUserAddCommand : TeamEnterpriseUserCommand
    {
        public TeamEnterpriseUserAddCommand() : base("team_enterprise_user_add")
        {
        }
        [DataMember(Name = "user_type")]
        public int UserType { get; set; }

        [DataMember(Name = "team_key", EmitDefaultValue = false)]
        public string TeamKey { get; set; }
    }
}
