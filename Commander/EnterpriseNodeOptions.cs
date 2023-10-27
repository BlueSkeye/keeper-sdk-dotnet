using CommandLine;

namespace Commander
{
    internal class EnterpriseNodeOptions : EnterpriseGenericOptions
    {
        [Value(0, Required = false, HelpText = "enterprise-user command: \"--command=[tree, add, update, delete]\" <Node name or ID>")]
        public string Node { get; set; }

        [Option("command", Required = false, HelpText = "[tree, add, update, delete]")]
        public string Command { get; set; }

        [Option("parent", Required = false, HelpText = "parent node name or ID")]
        public string Parent { get; set; }

        [Option("name", Required = false, HelpText = "new node display name")]
        public string Name { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "verbose output")]
        public bool Verbose { get; set; }

        [Option("toggle-isolated", Required = false, HelpText = "toggle node isolation flag")]
        public bool RestrictVisibility { get; set; }
    }
}
