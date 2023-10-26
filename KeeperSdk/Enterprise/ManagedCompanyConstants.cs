
namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public static class ManagedCompanyConstants
    {
        public static readonly MspProduct[] MspProducts = new[]
        {
            new MspProduct
            {
                ProductCode = BusinessLicense,
                ProductName = "Business",
                FilePlanCode = StoragePlan100GB,
            },
            new MspProduct
            {
                ProductCode = BusinessPlusLicense,
                ProductName = "Business Plus",
                FilePlanCode = StoragePlan1TB,
            },
            new MspProduct
            {
                ProductCode = EnterpriseLicense,
                ProductName = "Enterprise",
                FilePlanCode = StoragePlan100GB,
            },
            new MspProduct
            {
                ProductCode = EnterprisePlusLicense,
                ProductName = "Enterprise Plus",
                FilePlanCode = StoragePlan1TB,
            },
        };
        public static readonly MspFilePlan[] MspFilePlans = new[]
        {
            new MspFilePlan
            {
                FilePlanCode = StoragePlan100GB,
                FilePlanName = "100GB",
            },
            new MspFilePlan
            {
                FilePlanCode = StoragePlan1TB,
                FilePlanName = "1TB",
            },
            new MspFilePlan
            {
                FilePlanCode = StoragePlan10TB,
                FilePlanName = "10TB",
            },
        };

        public static readonly MspAddon[] MspAddons = new[]
        {
            new MspAddon
            {
                AddonCode = AddonBreachWatch,
                AddonName = "BreachWatch",
                SeatsRequired = false,
            },
            new MspAddon
            {
                AddonCode = AddonComplianceReport,
                AddonName = "Compliance Reporting",
                SeatsRequired = false,
            },
            new MspAddon
            {
                AddonCode = AddonAuditReport,
                AddonName = "Advanced Reporting & Alerts Module",
                SeatsRequired = false,
            },
            new MspAddon
            {
                AddonCode = AddonServiceAndSupport,
                AddonName = "MSP Dedicated Service & Support",
                SeatsRequired = false,
            },
            new MspAddon
            {
                AddonCode = AddonSecretsManager,
                AddonName = "Keeper Secrets Manager (KSM)",
                SeatsRequired = false,
            },
            new MspAddon
            {
                AddonCode = AddonConnectionManager,
                AddonName = "Keeper Connection Manager (KCM)",
                SeatsRequired = true,
            },
            new MspAddon
            {
                AddonCode = AddonChat,
                AddonName = "KeeperChat",
                SeatsRequired = false,
            },
        };

        public const string BusinessLicense = "business";
        public const string BusinessPlusLicense = "businessPlus";
        public const string EnterpriseLicense = "enterprise";
        public const string EnterprisePlusLicense = "enterprisePlus";

        public const string StoragePlan100GB = "STORAGE_100GB";
        public const string StoragePlan1TB = "STORAGE_1000GB";
        public const string StoragePlan10TB = "STORAGE_10000GB";

        public const string AddonBreachWatch = "enterprise_breach_watch";
        public const string AddonComplianceReport = "compliance_report";
        public const string AddonAuditReport = "enterprise_audit_and_reporting";
        public const string AddonServiceAndSupport = "msp_service_and_support";
        public const string AddonSecretsManager = "secrets_manager";
        public const string AddonConnectionManager = "connection_manager";
        public const string AddonChat = "chat";
    }
}
