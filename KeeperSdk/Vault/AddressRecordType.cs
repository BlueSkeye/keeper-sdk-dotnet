using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <exclude />
    public class AddressRecordType : TypedRecordFileRef
    {
        private TypedField<FieldTypeAddress> _address;

        public FieldTypeAddress Address => _address.TypedValue;

        protected internal override void LoadTypedField(ITypedField field)
        {
            if (field.FieldName == "address" && _address == null)
            {
                _address = field as TypedField<FieldTypeAddress>;
            }
            else
            {
                base.LoadTypedField(field);
            }
        }

        protected internal override IEnumerable<ITypedField> CreateMissingFields()
        {
            if (_address == null)
            {
                _address = new TypedField<FieldTypeAddress>("address");
                yield return _address;
            }

            foreach (var f in base.CreateMissingFields())
            {
                yield return f;
            }
        }

        protected internal override string RecordType => "address";
    }
}
