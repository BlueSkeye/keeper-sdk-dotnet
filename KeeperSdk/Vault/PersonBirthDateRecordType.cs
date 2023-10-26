using KeeperSecurity.Utils;
using System;
using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <exclude />
    public class PersonBirthDateRecordType : TypedRecordFileRef
    {
        private TypedField<FieldTypeName> _name;
        private TypedField<long> _birthDate;

        public FieldTypeName Name => _name.TypedValue;

        public DateTimeOffset BirthDate
        {
            get => DateTimeOffsetExtensions.FromUnixTimeMilliseconds(_birthDate.TypedValue);
            set => _birthDate.TypedValue = value.ToUnixTimeMilliseconds();
        }

        protected internal override void LoadTypedField(ITypedField field)
        {
            if (field.FieldName == "name" && _name == null)
            {
                _name = field as TypedField<FieldTypeName>;
            }
            else if (field.FieldName == "birthDate" && _birthDate == null)
            {
                _birthDate = field as TypedField<long>;
            }
            else
            {
                base.LoadTypedField(field);
            }
        }

        protected internal override IEnumerable<ITypedField> CreateMissingFields()
        {
            if (_name == null)
            {
                _name = new TypedField<FieldTypeName>("name");
                yield return _name;
            }

            if (_birthDate == null)
            {
                _birthDate = new TypedField<long>("birthDate");
                yield return _birthDate;
            }

            foreach (var f in base.CreateMissingFields())
            {
                yield return f;
            }
        }
    }
}
