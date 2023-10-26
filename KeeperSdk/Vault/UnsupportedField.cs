
namespace KeeperSecurity.Vault
{
    /// <exclude />
    public class UnsupportedField : ITypedField, IToRecordTypeDataField
    {
        private readonly RecordTypeDataFieldBase _dataField;
        internal UnsupportedField(RecordTypeDataFieldBase dataField)
        {
            _dataField = dataField;
        }

        RecordTypeDataFieldBase IToRecordTypeDataField.ToRecordTypeDataField()
        {
            return _dataField;
        }

        object ITypedField.ObjectValue
        {
            get => null;
            set { }
        }

        object ITypedField.AppendValue()
        {
            return null;
        }

        object ITypedField.GetValueAt(int index)
        {
            return null;
        }

        void ITypedField.SetValueAt(int index, object value)
        {
        }

        void ITypedField.DeleteValueAt(int index)
        {
        }
        int ITypedField.Count => 0;

        string IRecordTypeField.FieldName => _dataField.Type;
        string IRecordTypeField.FieldLabel => _dataField.Label;

        string ICustomField.Type => _dataField.Type;
        string ICustomField.Name => _dataField.Label;
        string ICustomField.Value
        {
            get => null;
            set { }
        }
    }
}
