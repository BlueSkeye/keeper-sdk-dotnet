using KeeperSecurity.Commands;

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    ///     Represents Enterprise Role
    /// </summary>
    public class EnterpriseRole : IEnterpriseEntity, IParentNodeEntity, IEncryptedData, IDisplayName
    {
        /// <summary>
        ///     Role ID.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        ///     User Name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Role is visible to the subnodes.
        /// </summary>
        public bool VisibleBelow { get; set; }

        /// <summary>
        /// New users automaticall added to this role
        /// </summary>
        public bool NewUserInherit { get; set; }

        /// <exclude/>
        public string RoleType { get; set; }
        internal string KeyType { get; set; }
        /// <summary>
        ///     Node that owns the role.
        /// </summary>
        public long ParentNodeId { get; set; }
        public string EncryptedData { get; set; }
    }
}
