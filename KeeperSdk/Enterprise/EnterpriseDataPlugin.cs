using System.Collections.Generic;

namespace KeeperSecurity.Enterprise
{
    /// <exclude/>
    public abstract class EnterpriseDataPlugin
    {
        public IEnterpriseLoader Enterprise { get; internal set; }
        public abstract IEnumerable<IKeeperEnterpriseEntity> Entities { get; }
    }
}
