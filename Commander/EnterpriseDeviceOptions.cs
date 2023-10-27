using CommandLine;

namespace Commander
{
    internal class EnterpriseDeviceOptions : EnterpriseGenericOptions
    {
        [Option("auto-approve", Required = false, Default = null, HelpText = "auto approve devices")]
        public bool? AutoApprove { get; set; }

        [Value(0, Required = false, HelpText = "command: \"list\", \"approve\", \"decline\"")]
        public string Command { get; set; }

        [Value(1, Required = false, HelpText = "device approval request: \"all\", email, or device id")]
        public string Match { get; set; }
    }

}
