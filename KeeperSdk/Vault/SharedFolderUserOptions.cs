
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines shared folder user permissions.
    /// </summary>

    public class SharedFolderUserOptions : ISharedFolderUserOptions
    {
        public bool? ManageRecords { get; set; }
        public bool? ManageUsers { get; set; }
    }
}
