using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents folder.
    /// </summary>
    public class FolderNode
    {
        /// <summary>
        /// Folder UID.
        /// </summary>
        public string FolderUid { get; internal set; }
        /// <summary>
        /// Parent folder UID.
        /// </summary>
        public string ParentUid { get; internal set; }
        /// <summary>
        /// Shared Folder UID. 
        /// </summary>
        /// <remarks>Populated for <c>SharedFolderFolder</c> <see cref="FolderType"/></remarks>
        public string SharedFolderUid { get; internal set; }
        /// <summary>
        /// Folder type.
        /// </summary>
        public FolderType FolderType { get; internal set; } = FolderType.UserFolder;
        /// <summary>
        /// Folder name.
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// A UID list of subfolders
        /// </summary>
        public IList<string> Subfolders { get; } = new List<string>();
        /// <summary>
        /// A UID list of records.
        /// </summary>
        public IList<string> Records { get; } = new List<string>();

        /// <summary>
        /// Folder key
        /// </summary>
        public byte[] FolderKey { get; internal set; }
    }
}
