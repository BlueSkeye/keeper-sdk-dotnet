using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <exclude />
    public class LoginRecordType : TypedRecordFileRef
    {
        private TypedField<string> _login;
        private TypedField<string> _password;
        private TypedField<string> _url;
        private TypedField<string> _oneTimeCode;

        public string Login
        {
            get => _login.TypedValue;
            set => _login.TypedValue = value;
        }

        public string Password
        {
            get => _password.TypedValue;
            set => _password.TypedValue = value;
        }

        public string Url
        {
            get => _url.TypedValue;
            set => _url.TypedValue = value;
        }

        public string OneTimeCode
        {
            get => _oneTimeCode.TypedValue;
            set => _oneTimeCode.TypedValue = value;
        }

        protected internal override void LoadTypedField(ITypedField field)
        {
            if (field.FieldName == "login" && _login == null)
            {
                _login = field as TypedField<string>;
            }
            else if (field.FieldName == "password" && _password == null)
            {
                _password = field as TypedField<string>;
            }
            else if (field.FieldName == "url" && _url == null)
            {
                _url = field as TypedField<string>;
            }
            else if (field.FieldName == "oneTimeCode" && _oneTimeCode == null)
            {
                _oneTimeCode = field as TypedField<string>;
            }
            else
            {
                base.LoadTypedField(field);
            }
        }

        protected internal override IEnumerable<ITypedField> CreateMissingFields()
        {
            if (_login == null)
            {
                _login = new TypedField<string>("login");
                yield return _login;
            }

            if (_password == null)
            {
                _password = new TypedField<string>("password");
                yield return _password;
            }

            if (_url == null)
            {
                _url = new TypedField<string>("url");
                yield return _url;
            }

            if (_oneTimeCode == null)
            {
                _oneTimeCode = new TypedField<string>("oneTimeCode");
                yield return _oneTimeCode;
            }

            foreach (var f in base.CreateMissingFields())
            {
                yield return f;
            }
        }

        protected internal override string RecordType => "login";
    }
}
