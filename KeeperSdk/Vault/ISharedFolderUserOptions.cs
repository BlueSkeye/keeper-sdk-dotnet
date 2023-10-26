using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines shared folder user permissions.
    /// </summary>
    public interface ISharedFolderUserOptions
    {
        /// <summary>
        /// User can manage other users.
        /// </summary>
        bool? ManageUsers { get; }
        /// <summary>
        /// User can manage records.
        /// </summary>
        bool? ManageRecords { get; }
    }
}
