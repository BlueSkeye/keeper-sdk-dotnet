using System;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public interface IGetEnterprise
    {
        Func<IEnterpriseLoader> GetEnterprise { get; set; }
    }
}
