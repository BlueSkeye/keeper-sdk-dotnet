using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents Shared Folder.
    /// </summary>
    public class SharedFolder
    {
        /// <summary>
        /// Shared folder UID.
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// Shared folder name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Default manage records permission.
        /// </summary>
        public bool DefaultManageRecords { get; set; }
        /// <summary>
        /// Default manage users permission.
        /// </summary>
        public bool DefaultManageUsers { get; set; }
        /// <summary>
        /// Default record can be re-shared permission.
        /// </summary>
        public bool DefaultCanEdit { get; set; }
        /// <summary>
        /// Default record can be edited permission.
        /// </summary>
        public bool DefaultCanShare { get; set; }

        /// <summary>
        /// A list of user permissions.
        /// </summary>
        public List<SharedFolderPermission> UsersPermissions { get; } = new List<SharedFolderPermission>();
        /// <summary>
        /// A list of record permissions.
        /// </summary>
        public List<SharedFolderRecord> RecordPermissions { get; } = new List<SharedFolderRecord>();

        /// <summary>
        /// Shared Folder key.
        /// </summary>
        public byte[] SharedFolderKey { get; set; }
    }
}
