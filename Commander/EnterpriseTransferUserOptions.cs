using CommandLine;

namespace Commander
{
    internal class EnterpriseTransferUserOptions : EnterpriseGenericOptions
    {
        [Value(0, Required = true, HelpText = "email or user ID to transfer vault from user")]
        public string FromUser { get; set; }

        [Value(1, Required = true, HelpText = "email or user ID to transfer vault to user")]
        public string TargetUser { get; set; }
    }

}
