using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    [DataContract]
    internal class RecordFileData
    {
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "size", EmitDefaultValue = false)]
        public long? Size { get; set; }

        [DataMember(Name = "thumbnail_size", EmitDefaultValue = false)]
        public long? ThumbnailSize { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        [DataMember(Name = "lastModified", EmitDefaultValue = false)]
        public long? LastModified { get; set; }
    }
}
