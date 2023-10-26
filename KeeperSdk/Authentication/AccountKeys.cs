using KeeperSecurity.Utils;

namespace KeeperSecurity.Authentication
{
    internal class AccountKeys
    {
        public string EncryptionParams { get; internal set; }

        public string EncryptedDataKey { get; internal set; }

        public string EncryptedPrivateKey { get; internal set; }

        public string EncryptedEcPrivateKey { get; internal set; }

        public double? DataKeyBackupDate { get; internal set; }

        internal static AccountKeys LoadFromProtobuf(AccountSummary.KeysInfo keyInfo)
        {
            return new AccountKeys
            {
                EncryptionParams = keyInfo.EncryptionParams.ToByteArray().Base64UrlEncode(),
                EncryptedPrivateKey = keyInfo.EncryptedPrivateKey.ToByteArray().Base64UrlEncode(),
                EncryptedEcPrivateKey = keyInfo.EncryptedEccPrivateKey?.Length > 0 ? keyInfo.EncryptedEccPrivateKey.ToByteArray().Base64UrlEncode() : null,
                EncryptedDataKey = keyInfo.EncryptedDataKey.ToByteArray().Base64UrlEncode(),
                DataKeyBackupDate = keyInfo.DataKeyBackupDate > 1 ? keyInfo.DataKeyBackupDate : (double?) null
            };
        }
    }
}
