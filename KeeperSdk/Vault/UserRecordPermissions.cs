
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents record permissions for user.
    /// </summary>
    public class UserRecordPermissions
    {
        /// <summary>
        /// Keeper username.
        /// </summary>
        public string Username { get; internal set; }
        /// <summary>
        /// Flag indicating if the user has share permissions.
        /// </summary>
        public bool CanShare { get; internal set; }
        /// <summary>
        /// Flag indicating if the user has rights to edit the record
        /// </summary>
        public bool CanEdit { get; internal set; }
        /// <summary>
        /// Flag indicating if the user is record owner.
        /// </summary>
        public bool Owner { get; internal set; }
        /// <summary>
        /// Flag indicating if the user has pending invitation.
        /// </summary>
        public bool AwaitingApproval { get; internal set; }
    }
}
