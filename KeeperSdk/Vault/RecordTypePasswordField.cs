using KeeperSecurity.Utils;

namespace KeeperSecurity.Vault
{
    public class RecordTypePasswordField : RecordTypeField
    {
        public RecordTypePasswordField(RecordField recordField, string label) : base(recordField, label)
        {
        }
        public PasswordGenerationOptions PasswordOptions { get; set; }
    }
}
