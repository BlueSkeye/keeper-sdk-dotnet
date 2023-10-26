using KeeperSecurity.Utils;
using System;
using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <exclude />
    public class PassportRecordType : PersonBirthDateRecordType
    {
        private TypedField<string> _passportNumber;
        private TypedField<long> _expirationDate;
        private TypedField<long> _dateIssued;
        private TypedField<string> _password;
        private TypedField<string> _addressRef;

        public string PassportNumber
        {
            get => _passportNumber.TypedValue;
            set => _passportNumber.TypedValue = value;
        }

        public DateTimeOffset ExpirationDate
        {
            get => DateTimeOffsetExtensions.FromUnixTimeMilliseconds(_expirationDate.TypedValue);
            set => _expirationDate.TypedValue = value.ToUnixTimeMilliseconds();
        }

        public DateTimeOffset DateIssued
        {
            get => DateTimeOffsetExtensions.FromUnixTimeMilliseconds(_dateIssued.TypedValue);
            set => _dateIssued.TypedValue = value.ToUnixTimeMilliseconds();
        }

        public string Password
        {
            get => _password.TypedValue;
            set => _password.TypedValue = value;
        }

        public string AddressRef
        {
            get => _addressRef.TypedValue;
            set => _addressRef.TypedValue = value;
        }

        protected internal override void LoadTypedField(ITypedField field)
        {
            if (field.FieldName == "accountNumber" && field.FieldLabel == "passportNumber" && _passportNumber == null)
            {
                _passportNumber = field as TypedField<string>;
            }
            else if (field.FieldName == "expirationDate" && _expirationDate == null)
            {
                _expirationDate = field as TypedField<long>;
            }
            else if (field.FieldName == "date" && field.FieldLabel == "dateIssued" && _dateIssued == null)
            {
                _dateIssued = field as TypedField<long>;
            }
            else if (field.FieldName == "password" && _password == null)
            {
                _password = field as TypedField<string>;
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
            if (_passportNumber == null)
            {
                _passportNumber = new TypedField<string>("accountNumber", "passportNumber");
                yield return _passportNumber;
            }

            if (_expirationDate == null)
            {
                _expirationDate = new TypedField<long>("expirationDate");
                yield return _expirationDate;
            }

            if (_dateIssued == null)
            {
                _dateIssued = new TypedField<long>("date", "dateIssued");
                yield return _dateIssued;
            }

            if (_password == null)
            {
                _password = new TypedField<string>("password");
                yield return _password;
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
        protected internal override string RecordType => "passport";
    }
}
