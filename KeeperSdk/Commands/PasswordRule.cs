using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class PasswordRule
    {
        [DataMember(Name = "match")]
        public bool match;

        [DataMember(Name = "pattern")]
        public string pattern;

        [DataMember(Name = "description")]
        public string description;

        [DataMember(Name = "rule_type")]
        public string ruleType;
    }
}
