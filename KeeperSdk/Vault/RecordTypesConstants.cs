using KeeperSecurity.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Record Types Schema: Fields
    /// </summary>
    public static class RecordTypesConstants
    {
        private static readonly Dictionary<string, FieldType> _fieldTypes = new Dictionary<string, FieldType>(StringComparer.InvariantCultureIgnoreCase);
        private static readonly Dictionary<string, RecordField> _recordFields = new Dictionary<string, RecordField>(StringComparer.InvariantCultureIgnoreCase);

        static RecordTypesConstants()
        {
            var types = new[]
            {
                new FieldType("text", typeof(string), "plain text"),
                new FieldType("url", typeof(string), "url string, can be clicked"),
                new FieldType("multiline", typeof(string), "multiline text"),
                new FieldType("fileRef", typeof(string), "reference to the file field on another record"),
                new FieldType("email", typeof(string), "valid email address plus tag"),
                new FieldType("host", typeof(FieldTypeHost), "multiple fields to capture host information"),
                new FieldType("phone", typeof(FieldTypePhone), "numbers and symbols only plus tag"),
                new FieldType("name", typeof(FieldTypeName), "multiple fields to capture name"),
                new FieldType("address", typeof(FieldTypeAddress), "multiple fields to capture address"),
                new FieldType("addressRef", typeof(string), "reference to the address field on another record"),
                new FieldType("cardRef", typeof(string), "reference to the card record type"),
                new FieldType("secret", typeof(string), "the field value is masked"),
                new FieldType("login", typeof(string), "Login field, detected as the website login for browser extension or KFFA."),
                new FieldType("password", typeof(string), "Field value is masked and allows for generation. Also complexity enforcements."),
                new FieldType("securityQuestion", typeof(FieldTypeSecurityQuestion), "Security Question and Answer"),
                new FieldType("otp", typeof(string), "captures the seed, displays QR code"),
                new FieldType("paymentCard", typeof(FieldTypePaymentCard), "Field consisting of validated card number, expiration date and security code."),
                new FieldType("date", typeof(long), "calendar date with validation, stored as unix milliseconds"),
                new FieldType("bankAccount", typeof(FieldTypeBankAccount), "bank account information"),
                new FieldType("privateKey", typeof(FieldTypeKeyPair), "private and/or public keys in ASN.1 format"),
                new FieldType("checkbox", typeof(bool), "on/off checkbox"),
                new FieldType("dropdown", typeof(string), "list of text choices"),
            };

            foreach (var t in types)
            {
                _fieldTypes.Add(t.Name, t);
            }

            var fields = new[]
            {
                new RecordField("text", _fieldTypes["text"]),
                new RecordField("title", _fieldTypes["text"]),
                new RecordField("login", _fieldTypes["login"]),
                new RecordField("password", _fieldTypes["password"]),
                new RecordField("name", _fieldTypes["name"]),
                new RecordField("company", _fieldTypes["text"]),
                new RecordField("phone", _fieldTypes["phone"], RecordFieldMultiple.Optional),
                new RecordField("email", _fieldTypes["email"], RecordFieldMultiple.Optional),
                new RecordField("address", _fieldTypes["address"]),
                new RecordField("addressRef", _fieldTypes["addressRef"]),
                new RecordField("date", _fieldTypes["date"]),
                new RecordField("expirationDate", _fieldTypes["date"]),
                new RecordField("birthDate", _fieldTypes["date"]),
                new RecordField("paymentCard", _fieldTypes["paymentCard"]),
                new RecordField("accountNumber", _fieldTypes["text"]),
                new RecordField("bankAccount", _fieldTypes["bankAccount"]),
                new RecordField("cardRef", _fieldTypes["cardRef"], RecordFieldMultiple.Default),
                new RecordField("note", _fieldTypes["multiline"]),
                new RecordField("url", _fieldTypes["url"], RecordFieldMultiple.Optional),
                new RecordField("fileRef", _fieldTypes["fileRef"], RecordFieldMultiple.Default),
                new RecordField("host", _fieldTypes["host"], RecordFieldMultiple.Optional),
                new RecordField("securityQuestion", _fieldTypes["securityQuestion"], RecordFieldMultiple.Default),
                new RecordField("pinCode", _fieldTypes["secret"]),
                new RecordField("secret", _fieldTypes["secret"]),
                new RecordField("oneTimeCode", _fieldTypes["otp"]),
                new RecordField("keyPair", _fieldTypes["privateKey"]),
                new RecordField("licenseNumber", _fieldTypes["multiline"]),
                new RecordField("isSSIDHidden", _fieldTypes["checkbox"]),
                new RecordField("wifiEncryption", _fieldTypes["dropdown"]),
            };
            foreach (var rf in fields)
            {
                _recordFields.Add(rf.Name, rf);
            }
        }

        /// <summary>
        /// Gets supported Field Types
        /// </summary>
        public static IEnumerable<FieldType> FieldTypes => _fieldTypes.Values;

        /// <summary>
        /// Gets supported Fields
        /// </summary>
        public static IEnumerable<RecordField> RecordFields => _recordFields.Values;
        public static bool TryGetRecordField(string name, out RecordField value)
        {
            return _recordFields.TryGetValue(name ?? "text", out value);
        }

        private static readonly Dictionary<Type, RecordTypeInfo> _recordTypeInfo = new Dictionary<Type, RecordTypeInfo>();

        internal static bool GetRecordType(Type dataType, out RecordTypeInfo recordTypeInfo)
        {
            lock (_recordTypeInfo)
            {
                if (_recordTypeInfo.TryGetValue(dataType, out recordTypeInfo))
                {
                    return true;
                }
                var genericRecordType = typeof(RecordTypeDataField<>);
                var genericTypedFieldType = typeof(TypedField<>);
                recordTypeInfo = new RecordTypeInfo
                {
                    RecordFieldType = genericRecordType.MakeGenericType(dataType),
                    TypedFieldType = genericTypedFieldType.MakeGenericType(dataType),
                };
                recordTypeInfo.Serializer = new DataContractJsonSerializer(recordTypeInfo.RecordFieldType, JsonUtils.JsonSettings);
                _recordTypeInfo.Add(dataType, recordTypeInfo);
                return true;
            }
        }

        /// <exclude />
        public static bool GetTypedFieldType(Type dataType, out Type typedFieldType)
        {
            if (GetRecordType(dataType, out var rt))
            {
                typedFieldType = rt.TypedFieldType;
                return true;
            }

            typedFieldType = null;
            return false;
        }

        /// <exclude />
        public static bool GetRecordFieldDataType(Type dataType, out Type recordFieldType)
        {
            if (GetRecordType(dataType, out var rt))
            {
                recordFieldType = rt.RecordFieldType;
                return true;
            }

            recordFieldType = null;
            return false;
        }
        /// <exclude />
        public static bool GetJsonParser(Type dataType, out DataContractJsonSerializer jsonType)
        {
            if (GetRecordType(dataType, out var rt))
            {
                jsonType = rt.Serializer;
                return true;
            }

            jsonType = null;
            return false;
        }
    }
}
