using CommandLine;

namespace Commander
{
    internal class EnterpriseTeamOptions : EnterpriseGenericOptions
    {
        [Option("node", Required = false, HelpText = "node name or ID. \"add\", \"update\"")]
        public string Node { get; set; }

        [Option('q', "queued", Required = false, HelpText = "include queued team/user information. \"list\", \"view\"")]
        public bool Queued { get; set; }

        [Option("restrict-edit", Required = false, HelpText = "ON | OFF:  disable record edits. \"add\", \"update\"")]
        public string RestrictEdit { get; set; }

        [Option("restrict-share", Required = false, HelpText = "ON | OFF:  disable record re-shares. \"add\", \"update\"")]
        public string RestrictShare { get; set; }

        [Option("restrict-view", Required = false, HelpText = "ON | OFF:  disable view/copy passwords. \"add\", \"update\"")]
        public string RestrictView { get; set; }

        [Value(0, Required = false, HelpText = "enterprise-team command: \"list\", \"view\", \"add\", \"delete\", \"update\"")]
        public string Command { get; set; }

        [Value(1, Required = false, HelpText = "enterprise team Name, UID, list match")]
        public string Name { get; set; }
    }

}
