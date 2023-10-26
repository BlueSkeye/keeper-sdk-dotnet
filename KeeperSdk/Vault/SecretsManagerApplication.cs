
namespace KeeperSecurity.Vault
{
    public class SecretsManagerApplication : ApplicationRecord
    {
        public SecretsManagerDevice[] Devices { get; internal set; }
        public SecretManagerShare[] Shares { get; internal set; }
        public bool IsExternalShare { get; internal set; }
    }
}
