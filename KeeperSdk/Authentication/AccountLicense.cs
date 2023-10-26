
namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Represents user's account license information.
    /// </summary>
    public class AccountLicense
    {
        /// <summary>
        /// Account Type ID.
        /// <list type="bullet">
        /// <item>
        /// <term>0</term>
        /// <description>Consumer</description>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <description>Family</description>
        /// </item>
        /// <item>
        /// <term>2</term>
        /// <description>Enterprise</description>
        /// </item>
        /// </list>
        /// </summary>
        public int AccountType { get; internal set; }

        /// <summary>
        /// Product Type ID.
        /// <list type="bullet">
        /// <item>
        /// <term>1</term>
        /// <description>Trial</description>
        /// </item>
        /// <item>
        /// <term>2</term>
        /// <description>Backup</description>
        /// </item>
        /// <item>
        /// <term>3</term>
        /// <description>Groups</description>
        /// </item>
        /// <item>
        /// <term>4</term>
        /// <description>Backup unlimited</description>
        /// </item>
        /// </list>
        /// </summary>
        public int ProductTypeId { get; internal set; }

        /// <summary>
        /// Product Type name.
        /// </summary>
        public string ProductTypeName { get; internal set; }

        /// <summary>
        /// The date that the license will expire.
        /// </summary>
        public string ExpirationDate { get; internal set; }

        /// <summary>
        /// The number of seconds until this user’s subscription expires. Unix time.
        /// </summary>
        public float SecondsUntilExpiration { get; internal set; }

        /// <summary>
        /// The type of file plan the user has.
        /// </summary>
        public int FilePlanType { get; internal set; }

        /// <summary>
        /// The date that the file plan license will expire.
        /// </summary>
        public string StorageExpirationDate { get; internal set; }

        /// <summary>
        /// The number of seconds until this user’s file plan subscription expires. Unix time.
        /// </summary>
        public float SecondsUntilStorageExpiration { get; internal set; }

        /// <summary>
        /// File storage plan. Total bytes
        /// </summary>
        public long BytesTotal { get; internal set; }

        /// <summary>
        /// File storage plan. Used bytes
        /// </summary>
        public long BytesUsed { get; set; }

        internal static AccountLicense LoadFromProtobuf(AccountSummary.License license)
        {
            return new AccountLicense
            {
                AccountType = license.AccountType,
                ProductTypeId = license.ProductTypeId,
                ProductTypeName = license.ProductTypeName,
                ExpirationDate = license.ExpirationDate,
                SecondsUntilExpiration = license.SecondsUntilExpiration,
                FilePlanType = license.FilePlanType,
                StorageExpirationDate = license.StorageExpirationDate,
                SecondsUntilStorageExpiration = license.SecondsUntilStorageExpiration,
                BytesTotal = license.BytesTotal,
                BytesUsed = license.BytesUsed,
            };
        }
    }
}
