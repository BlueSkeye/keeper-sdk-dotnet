using System;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class SqlTableAttribute : Attribute
    {
        public string Name { get; set; }
        public string[] PrimaryKey { get; set; }
        public string[] Index1 { get; set; }
        public string[] Index2 { get; set; }
    }
}
