using System;

namespace KeeperSecurity.Vault
{
    public class SecretManagerShare
    {
        public string SecretUid { get; internal set; }
        public SecretManagerSecretType SecretType { get; internal set; }
        public bool Editable { get; internal set; }
        public DateTimeOffset CreatedOn { get; internal set; }
    }
}
