using System;
using System.Collections.Generic;
using System.Linq;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents a typed field.
    /// </summary>
    /// <typeparam name="T">Field Data Type</typeparam>
    public class TypedField<T> : ITypedField, IToRecordTypeDataField
    {
        internal TypedField(RecordTypeDataField<T> dataField)
        {
            FieldName = dataField.Type;
            FieldLabel = dataField.Label;
            if (dataField.Value != null)
            {
                Values.AddRange(dataField.Value);
            }
        }

        /// <exclude/>
        public TypedField() : this("")
        {
        }

        /// <exclude/>
        public TypedField(string fieldType, string fieldLabel = null)
        {
            FieldName = string.IsNullOrEmpty(fieldType) ? "text" : fieldType;
            FieldLabel = fieldLabel ?? "";
        }

        /// <summary>
        /// Field type name.
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// Field Label.
        /// </summary>
        public string FieldLabel { get; set; }

        /// <summary>
        /// Field values.
        /// </summary>
        public List<T> Values { get; } = new List<T>();

        public T AppendTypedValue()
        {
            switch (Values)
            {
                case List<string> ls:
                    ls.Add("");
                    break;
                case List<long> ll:
                    ll.Add(0);
                    break;
                default:
                    Values.Add((T) Activator.CreateInstance(typeof(T)));
                    break;
            }

            return Values.Last();
        }

        /// <summary>
        /// Default field value.
        /// </summary>
        public T TypedValue
        {
            get
            {
                if (Values.Count == 0)
                {
                    return AppendTypedValue();
                }

                return Values[0];
            }
            set
            {
                if (Values.Count == 0)
                {
                    Values.Add(value);
                }
                else
                {
                    Values[0] = value;
                }
            }
        }

        /// <exclude />
        public object ObjectValue
        {
            get => TypedValue;
            set => TypedValue = (T) value;
        }

        /// <summary>
        /// Gets field value at index
        /// </summary>
        /// <param name="index">value index</param>
        /// <returns></returns>
        public object GetValueAt(int index)
        {
            if (index >= 0 && index < Values.Count)
            {
                return Values[index];
            }

            return default(T);
        }

        /// <summary>
        /// Deletes field value at index
        /// </summary>
        /// <param name="index">Value index</param>
        public void DeleteValueAt(int index)
        {
            if (index >= 0 && index < Values.Count)
            {
                Values.RemoveAt(index);
            }
        }

        /// <summary>
        /// Sets field value at index
        /// </summary>
        /// <param name="index">Value index</param>
        /// <param name="value">Value</param>
        public void SetValueAt(int index, object value)
        {
            if (index >= 0 && index < Values.Count)
            {
                if (value is T tv)
                {
                    Values[index] = tv;
                }
            }
        }

        /// <summary>
        /// Value Count
        /// </summary>
        public int Count => Values.Count;

        RecordTypeDataFieldBase IToRecordTypeDataField.ToRecordTypeDataField()
        {
            return new RecordTypeDataField<T>(this);
        }

        /// <summary>
        /// Appends a value.
        /// </summary>
        /// <returns></returns>
        object ITypedField.AppendValue()
        {
            return AppendTypedValue();
        }

        string ICustomField.Name => FieldLabel;
        string ICustomField.Value
        {
            get => (TypedValue is string s) ? s : null;
            set => TypedValue = value is T t ? t : default;
        }

        string ICustomField.Type => FieldName;
    }
}
