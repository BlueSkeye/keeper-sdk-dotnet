using CommandLine;

namespace Commander
{
    internal class ManagedCompanyLoginOptions : EnterpriseGenericOptions
    {
        [Value(0, Required = true, HelpText = "mc-login <mc-company-id>")]
        public int CompanyId { get; set; }
    }

}
