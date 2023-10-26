using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class RecordExtra : IExtensibleDataObject
    {
        [DataMember(Name = "files", EmitDefaultValue = false)]
        public RecordExtraFile[] files;

        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public Dictionary<string, object>[] fields;

        public ExtensionDataObject ExtensionData { get; set; }
    }
}
