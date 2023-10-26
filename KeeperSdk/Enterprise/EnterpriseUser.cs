using KeeperSecurity.Commands;

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    ///     Represents Enterprise User
    /// </summary>
    public class EnterpriseUser : IEnterpriseEntity, IParentNodeEntity, IEncryptedData, IDisplayName
    {
        /// <summary>
        ///     User email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     User Status.
        /// </summary>
        public UserStatus UserStatus { get; internal set; }

        /// <summary>
        ///     User Name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     User ID.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        ///     Node that owns the user.
        /// </summary>
        public long ParentNodeId { get; internal set; }

        /// <exclude />
        public int UserId { get; internal set; }

        /// <exclude />
        public string KeyType { get; internal set; }

        /// <exclude />
        public string EncryptedData { get; internal set; }

        /// <exclude />
        public long AccountShareExpiration { get; internal set; }
    }
}
