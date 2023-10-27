using CommandLine;

namespace Commander
{
    internal class EnterpriseUserOptions : EnterpriseGenericOptions
    {
        [Option("team", Required = false, HelpText = "team name or UID. \"team-add\", \"team-remove\"")]
        public string Team { get; set; }

        [Option("node", Required = false, HelpText = "node name or ID. \"invite\"")]
        public string Node { get; set; }

        [Option("name", Required = false, HelpText = "user full name. \"invite\"")]
        public string FullName { get; set; }

        [Option("yes", Required = false, HelpText = "delete user without confirmation prompt. \"delete\"")]
        public bool Confirm { get; set; }

        [Value(0, Required = false, HelpText = "enterprise-user command: \"list\", \"view\", \"invite\", \"lock\", \"unlock\", \"team-add\", \"team-remove\", \"delete\"")]
        public string Command { get; set; }

        [Value(1, Required = false, HelpText = "enterprise user email, ID (except \"invite\")")]
        public string User { get; set; }
    }

}
