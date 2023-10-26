using System;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents generic Keeper Record
    /// </summary>
    public abstract class KeeperRecord
    {
        /// <summary>
        /// Record UID.
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// Record version
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Record revision
        /// </summary>
        public long Revision { get; set; }
        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Last modification time.
        /// </summary>
        public DateTimeOffset ClientModified { get; internal set; }
        /// <summary>
        /// Is user Owner?
        /// </summary>
        public bool Owner { get; set; }
        /// <summary>
        /// Is record Shared?
        /// </summary>
        public bool Shared { get; set; }
        /// <summary>
        /// Record key.
        /// </summary>
        public byte[] RecordKey { get; set; }
    }
}
