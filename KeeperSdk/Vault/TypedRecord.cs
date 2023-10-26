using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents a Typed Record 
    /// </summary>
    /// <seealso cref="ITypedField"/>
    public class TypedRecord : KeeperRecord
    {
        /// <summary>
        /// Record notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Record type name.
        /// </summary>
        public string TypeName { get; set; }

        /// <exclude/>
        public TypedRecord(string typeName)
        {
            TypeName = typeName;
        }
        /// <summary>
        /// Record mandatory fields.
        /// </summary>
        public List<ITypedField> Fields { get; } = new List<ITypedField>();
        /// <summary>
        /// Record custom data.
        /// </summary>
        public List<ITypedField> Custom { get; } = new List<ITypedField>();
    }
}
