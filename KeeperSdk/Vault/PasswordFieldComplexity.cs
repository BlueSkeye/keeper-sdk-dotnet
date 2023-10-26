using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    [DataContract]
    internal class PasswordFieldComplexity
    {
        [DataMember(Name = "length")]
        public int Length { get; set; }
        [DataMember(Name = "caps")]
        public int Upper { get; set; }
        [DataMember(Name = "lowercase")]
        public int Lower { get; set; }
        [DataMember(Name = "digits")]
        public int Digit { get; set; }
        [DataMember(Name = "special")]
        public int Special { get; set; }
    }
}
