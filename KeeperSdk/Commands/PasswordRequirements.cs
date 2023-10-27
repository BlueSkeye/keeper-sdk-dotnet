using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class PasswordRequirements : IPasswordRules
    {
        [DataMember(Name = "password_rules_intro", EmitDefaultValue = false)]
        public string PasswordRulesIntro { get; set; }

        [DataMember(Name = "password_rules", EmitDefaultValue = false)]
        public PasswordRule[] PasswordRules { get; set; }
    }
}
