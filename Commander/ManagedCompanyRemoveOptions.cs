using CommandLine;

namespace Commander
{
    internal class ManagedCompanyRemoveOptions : EnterpriseGenericOptions
    {
        [Value(0, Required = true, HelpText = "Managed company name or ID")]
        public string Company { get; set; }
    }

}
