using KeeperSecurity.Commands;
using System.Collections.Generic;

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    ///     Represents Enterprise Node.
    /// </summary>
    public class EnterpriseNode : IEnterpriseEntity, IParentNodeEntity, IEncryptedData, IDisplayName
    {
        /// <summary>
        ///     A list of child node IDs
        /// </summary>
        public ISet<long> Subnodes { get; } = new HashSet<long>();

        /// <summary>
        ///     Node Name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Node ID.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        ///     Parent Node ID.
        /// </summary>
        public long ParentNodeId { get; internal set; }

        /// <summary>
        ///     Node Isolation flag.
        /// </summary>
        public bool RestrictVisibility { get; internal set; }

        /// <exclude/>
        public string EncryptedData { get; internal set; }

        /// <exclude/>
        public long BridgeId { get; internal set; }

        /// <exclude/>
        public long ScimId { get; internal set; }

        /// <exclude/>
        public long[] SsoServiceProviderIds { get; internal set; }
    }
}
