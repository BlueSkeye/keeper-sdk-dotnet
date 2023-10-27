using CommandLine;

namespace Commander
{
    internal class ManagedCompanyCommonOptions : EnterpriseGenericOptions
    {
        [Option("product", Required = false, HelpText = "Product Plan: business, businessPlus, enterprise, enterprisePlus")]
        public string Product { get; set; }

        [Option("seats", Required = false, HelpText = "Maximum number of seats. -1 unlimited.")]
        public int? Seats { get; set; }

        [Option("node", Required = false, HelpText = "Node Name or ID.")]
        public string Node { get; set; }

        [Option("storage", Required = false, HelpText = "Storage Plan: 100GB, 1TB, 10TB")]
        public string Storage { get; set; }

        [Option("addons", Required = false, HelpText = "Comma-separated list of addons: \nenterprise_breach_watch, compliance_report, enterprise_audit_and_reporting, \nmsp_service_and_support, secrets_manager, connection_manager:N, chat")]
        public string Addons { get; set; }
    }

}
