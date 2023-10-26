using KeeperSecurity.Utils;
using System;
using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <exclude />
    public class DriverLicenseRecordType : PersonBirthDateRecordType
    {
        private TypedField<string> _dlNumber;
        private TypedField<long> _expirationDate;
        private TypedField<string> _addressRef;

        public string DlNumber
        {
            get => _dlNumber.TypedValue;
            set => _dlNumber.TypedValue = value;
        }

        public DateTimeOffset ExpirationDate
        {
            get => DateTimeOffsetExtensions.FromUnixTimeMilliseconds(_expirationDate.TypedValue);
            set => _expirationDate.TypedValue = value.ToUnixTimeMilliseconds();
        }

        public string AddressRef
        {
            get => _addressRef.TypedValue;
            set => _addressRef.TypedValue = value;
        }


        protected internal override void LoadTypedField(ITypedField field)
        {
            if (field.FieldName == "accountNumber" && field.FieldLabel == "dlNumber" && _dlNumber == null)
            {
                _dlNumber = field as TypedField<string>;
            }
            else if (field.FieldName == "expirationDate" && _expirationDate == null)
            {
                _expirationDate = field as TypedField<long>;
            }
            else if (field.FieldName == "addressRef" && _addressRef == null)
            {
                _addressRef = field as TypedField<string>;
            }
            else
            {
                base.LoadTypedField(field);
            }
        }

        protected internal override IEnumerable<ITypedField> CreateMissingFields()
        {
            if (_dlNumber == null)
            {
                _dlNumber = new TypedField<string>("accountNumber", "dlNumber");
                yield return _dlNumber;
            }

            if (_expirationDate == null)
            {
                _expirationDate = new TypedField<long>("expirationDate");
                yield return _expirationDate;
            }

            if (_addressRef == null)
            {
                _addressRef = new TypedField<string>("addressRef");
                yield return _addressRef;
            }

            foreach (var f in base.CreateMissingFields())
            {
                yield return f;
            }
        }
        protected internal override string RecordType => "driverLicense";
    }
}
