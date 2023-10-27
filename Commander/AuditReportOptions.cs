using CommandLine;
using System.Collections.Generic;

namespace Commander
{
    internal class AuditReportOptions
    {
        [Option("limit", Required = false, Default = 100, HelpText = "maximum number of returned events")]
        public int Limit { get; set; }

        [Option("created", Required = false, Default = null, HelpText = "event creation datetime")]
        public string Created { get; set; }

        [Option("event-type", Required = false, Default = null, Separator = ',', HelpText = "audit event type")]
        public IEnumerable<string> EventType { get; set; }

        [Option("username", Required = false, Default = null, HelpText = "username of event originator")]
        public string Username { get; set; }

        [Option("to_username", Required = false, Default = null, HelpText = "username of event target")]
        public string ToUsername { get; set; }

        [Option("record_uid", Required = false, Default = null, HelpText = "record UID")]
        public string RecordUid { get; set; }

        [Option("shared-folder-uid", Required = false, Default = null, HelpText = "shared folder UID")]
        public string SharedFolderUid { get; set; }
    }

}
