using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    [DataContract]
    internal class RecordTypeDataFieldBase : IExtensibleDataObject
    {
        [DataMember(Name = "type", Order = 1)]
        public string Type { get; set; }
        [DataMember(Name = "label", Order = 2, EmitDefaultValue = false)]
        public string Label { get; set; }
        public ExtensionDataObject ExtensionData { get; set; }

        public virtual ITypedField CreateTypedField()
        {
            return null;
        }
    }
}
