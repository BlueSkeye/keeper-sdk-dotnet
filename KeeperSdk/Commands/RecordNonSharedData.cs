using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class RecordNonSharedData : IExtensibleDataObject
    {
        public ExtensionDataObject ExtensionData { get; set; }
    }

}
