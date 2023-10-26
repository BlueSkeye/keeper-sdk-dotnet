using System.Collections.Generic;
using System.Linq;

namespace KeeperSecurity.Vault
{
    /// <summary>
    ///  Record Types Schema: Record Type definition.
    /// </summary>
    public class RecordType
    {
        /// <exclude />
        public RecordType() { }

        /// <exclude />
        public RecordType(int id, string name, string description, IEnumerable<RecordTypeField> fields) : this()
        {
            Id = id;
            Scope = RecordTypeScope.User;
            Name = name;
            Description = description;
            Fields = fields.ToArray();
        }

        /// <summary>
        /// Gets record type ID
        /// </summary>
        public int Id { get; internal set; }
        /// <summary>
        /// Gets record type scope
        /// </summary>
        public RecordTypeScope Scope { get; internal set; }
        /// <summary>
        /// Gets record type name
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets record type description
        /// </summary>
        public string Description { get; internal set; }
        /// <summary>
        /// Gets record type fields
        /// </summary>
        public RecordTypeField[] Fields { get; internal set; }
    }
}
