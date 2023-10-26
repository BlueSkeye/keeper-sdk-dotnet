using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents shared folder record permissions.
    /// </summary>
    public class SharedFolderRecordOptions : ISharedFolderRecordOptions
    {
        public bool? CanEdit { get; set; }
        public bool? CanShare { get; set; }
    }
}
