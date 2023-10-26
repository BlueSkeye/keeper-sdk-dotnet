using System.Linq;
using System.Runtime.Serialization;

namespace KeeperSecurity.Vault
{
    [DataContract]
    internal class RecordTypeDataField<T> : RecordTypeDataFieldBase
    {
        [DataMember(Name = "value", Order = 3, EmitDefaultValue = false)]
        public T[] Value { get; set; }

        public override ITypedField CreateTypedField()
        {
            return new TypedField<T>(this);
        }

        public RecordTypeDataField(TypedField<T> typedField)
        {
            Type = typedField.FieldName;
            Label = typedField.FieldLabel;
            Value = typedField.Values.Where(x => x != null).ToArray();
        }
    }
}
