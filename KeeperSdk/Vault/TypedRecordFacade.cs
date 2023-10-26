using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <exclude />
    public class TypedRecordFacade<T> where T : TypedRecordFields, new()
    {
        private readonly TypedRecord _typedRecord;

        public TypedRecordFacade(TypedRecord record = null)
        {
            Fields = new T();
            _typedRecord = record ?? new TypedRecord(Fields.RecordType);

            foreach (var field in _typedRecord.Fields)
            {
                Fields.LoadTypedField(field);
            }

            _typedRecord.Fields.AddRange(Fields.CreateMissingFields());
        }

        public TypedRecord TypedRecord => _typedRecord;
        public T Fields { get; }
        public IList<ITypedField> Custom => _typedRecord.Custom;
    }
}
