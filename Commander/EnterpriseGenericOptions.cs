using CommandLine;

namespace Commander
{
    internal class EnterpriseGenericOptions
    {
        [Option('f', "force", Required = false, Default = false, HelpText = "force reload enterprise data")]
        public bool Force { get; set; }
    }
}
