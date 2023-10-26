using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class ChangeMasterPasswordCommand : AuthenticatedCommand
    {
        [DataMember(Name = "auth_verifier")]
        public string AuthVerifier;

        [DataMember(Name = "encryption_params")]
        public string EncryptionParams;

        public ChangeMasterPasswordCommand() : base("change_master_password")
        {
        }
    }
}
