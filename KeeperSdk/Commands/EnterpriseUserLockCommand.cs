using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class EnterpriseUserLockCommand : EnterpriseUserCommand
    {
        public EnterpriseUserLockCommand() : base("enterprise_user_lock")
        {
        }

        [DataMember(Name = "lock", EmitDefaultValue = false)]
        public string Lock { get; set; }  // one of: locked, disabled, unlocked

        [DataMember(Name = "delete_if_pending", EmitDefaultValue = false)]
        public bool? DeleteIfPending { get; set; }
    }
}
