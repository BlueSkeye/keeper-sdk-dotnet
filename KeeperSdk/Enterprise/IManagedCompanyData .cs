using System.Collections.Generic;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public interface IManagedCompanyData
    {
        IEnumerable<EnterpriseManagedCompany> ManagedCompanies { get; }
    }
}
