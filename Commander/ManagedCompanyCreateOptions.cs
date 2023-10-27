using CommandLine;

namespace Commander
{
    internal class ManagedCompanyCreateOptions : ManagedCompanyCommonOptions
    {

        [Value(0, Required = true, HelpText = "Managed Company Name")]
        public string Name { get; set; }
    }

}
