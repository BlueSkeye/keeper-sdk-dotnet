using CommandLine;
using System.Collections.Generic;

namespace Commander
{
    internal class EnterpriseRoleOptions : EnterpriseGenericOptions
    {
        [Option("node", Required = false, HelpText = "Node Name or ID. \"add\"")]
        public string Node { get; set; }

        [Option('b', "visible-below", Required = false, Default = true, HelpText = "Visible to all nodes in hierarchy below. \"add\"")]
        public bool VisibleBelow { get; set; }

        [Option('n', "new-user", Required = false, Default = false, HelpText = "New users automatically get this role assigned. \"add\"")]
        public bool NewUser { get; set; }

        [Value(0, Required = false, HelpText = "command: \"list\", \"view\", \"add\", \"delete\", \"add-members\", \"remove-members\"")]
        public string Command { get; set; }

        [Value(1, Required = false, HelpText = "Role Name or ID")]
        public string Role { get; set; }

        [Value(2, Required = false, HelpText = "Command parameters:\n\"add-members\", \"remove-members\": list of User Emails, Team Names, User IDs, or Team UIDs. ")]
        public IEnumerable<string> Parameters { get; set; }
    }

}
