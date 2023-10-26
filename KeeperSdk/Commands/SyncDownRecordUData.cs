using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownRecordUData : IExtensibleDataObject
    {
        [DataMember(Name = "file_ids", EmitDefaultValue = false)]
        public string[] fileIds;

        [DataMember(Name = "file_size", EmitDefaultValue = false)]
        public long? FileSize { get; set; }

        [DataMember(Name = "thumbnail_size", EmitDefaultValue = false)]
        public long? ThumbnailSize { get; set; }

        public ExtensionDataObject ExtensionData { get; set; }
    }
}
