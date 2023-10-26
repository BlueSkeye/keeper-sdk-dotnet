
namespace KeeperSecurity.Vault
{
    /// <summary>
    ///  Defines shared folder user and record permissions.
    /// </summary>
    public class SharedFolderOptions : ISharedFolderRecordOptions, ISharedFolderUserOptions
    {
        public bool? CanEdit { get; set; }
        public bool? CanShare { get; set; }
        public bool? ManageUsers { get; set; }
        public bool? ManageRecords { get; set; }
    }
}
