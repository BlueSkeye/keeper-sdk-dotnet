using CommandLine;

namespace Commander
{
    internal class ManagedCompanyUpdateOptions : ManagedCompanyCommonOptions
    {
        [Option("name", Required = false, HelpText = "New Managed Company Name.")]
        public string Name { get; set; }

        [Value(0, Required = true, HelpText = "Managed company name or ID")]
        public string Company { get; set; }
    }

}
