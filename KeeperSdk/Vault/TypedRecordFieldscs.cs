using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <exclude />
    public class TypedRecordFields
    {
        private List<ITypedField> _overflow;

        protected internal virtual void LoadTypedField(ITypedField field)
        {
            if (_overflow == null)
            {
                _overflow = new List<ITypedField>();
            }

            _overflow.Add(field);
        }

        protected internal virtual IEnumerable<ITypedField> CreateMissingFields()
        {
            yield break;
        }

        protected internal virtual string RecordType => null;
    }
}
