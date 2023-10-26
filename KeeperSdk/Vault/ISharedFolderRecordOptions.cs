
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines shared folder record permissions.
    /// </summary>
    public interface ISharedFolderRecordOptions
    {
        /// <summary>
        /// Record can be edited.
        /// </summary>
        bool? CanEdit { get; }
        /// <summary>
        /// Record can be re-shared.
        /// </summary>
        bool? CanShare { get; }
    }
}
