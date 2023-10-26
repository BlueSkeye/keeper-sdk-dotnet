using System;
using System.Runtime.Serialization.Json;

namespace KeeperSecurity.Vault
{
    internal class RecordTypeInfo
    {
        public Type RecordFieldType { get; set; }   // RecordTypeDataField
        public Type TypedFieldType { get; set; }   // TypedField
        public DataContractJsonSerializer Serializer { get; set; }
    }
}
