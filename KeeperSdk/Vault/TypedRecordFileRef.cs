using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <exclude />
    public class TypedRecordFileRef : TypedRecordFields
    {
        public TypedField<string> FileRef { get; private set; }

        protected internal override void LoadTypedField(ITypedField field)
        {
            if (field.FieldName == "fileRef" && FileRef == null)
            {
                FileRef = field as TypedField<string>;
            }
            else
            {
                base.LoadTypedField(field);
            }
        }

        protected internal override IEnumerable<ITypedField> CreateMissingFields()
        {
            if (FileRef == null)
            {
                FileRef = new TypedField<string>("fileRef");
                yield return FileRef;
            }

            foreach (var f in base.CreateMissingFields())
            {
                yield return f;
            }
        }
    }
}
