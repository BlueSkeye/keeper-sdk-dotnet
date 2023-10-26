
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines Record Key Metadata properties.
    /// </summary>
    public interface IRecordMetadata : IUidLink
    {
        /// <summary>
        /// Record UID.
        /// </summary>
        string RecordUid { get; }
        /// <summary>
        /// Shared Folder UID if record key is encrypted with shared folder key.
        /// </summary>
        string SharedFolderUid { get; }
        /// <summary>
        /// Encrypted record key.
        /// </summary>
        string RecordKey { get; }
        /// <summary>
        /// Record key encryption key type.
        /// </summary>
        /// <seealso cref="KeyType"/>
        int RecordKeyType { get; }
        /// <summary>
        /// Can user re-share record?
        /// </summary>
        bool CanShare { get; set; }
        /// <summary>
        /// Can user edit record?
        /// </summary>
        bool CanEdit { get; set; }
    }
}
