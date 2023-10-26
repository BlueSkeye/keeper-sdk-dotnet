
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines Password Record properties.
    /// </summary>
    public interface IStorageRecord : IUid
    {
        /// <summary>
        /// Record UID.
        /// </summary>
        string RecordUid { get; }
        /// <summary>
        /// Last Revision.
        /// </summary>
        long Revision { get; }
        /// <summary>
        /// Record Version.
        /// 2 - Legacy
        /// 3 - Typed
        /// 4 - File
        /// 5 - Application
        /// </summary>
        int Version { get; }
        /// <summary>
        /// Last modification time. Unix epoch in seconds.
        /// </summary>
        long ClientModifiedTime { get; }
        /// <summary>
        /// Encrypted record data 
        /// </summary>
        string Data { get; set; }
        /// <summary>
        /// Encrypted record extra data.
        /// </summary>
        string Extra { get; }
        /// <summary>
        /// Unencrypted record data
        /// </summary>
        string Udata { get; }
        /// <summary>
        /// Is record shared?
        /// </summary>
        bool Shared { get; }
        /// <summary>
        /// Is user owner of the record?
        /// </summary>
        bool Owner { get; set; }
    }
}
