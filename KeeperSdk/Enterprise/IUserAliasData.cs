using System.Collections.Generic;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public interface IUserAliasData
    {
        IEnumerable<string> GetAliasesForUser(long userId);
    }
}
