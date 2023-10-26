using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SqlColumnAttribute : Attribute
    {
        public SqlColumnAttribute()
        {
            CanBeNull = true;
        }

        public string Name { get; set; }
        public int Length { get; set; }
        public bool CanBeNull { get; set; }
    }
}
