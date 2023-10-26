using System;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Record Types Schema: Field Type definition.
    /// </summary>
    public class FieldType
    {
        /// <exclude />
        public FieldType(string name, Type type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }

        /// <summary>
        /// Type name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Type description
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// .Net Type object
        /// </summary>
        public Type Type { get; }
    }
}
