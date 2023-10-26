using System.Collections.Generic;

namespace KeeperSecurity.Enterprise
{
    /// <exclude/>
    public interface IEnterpriseData
    {
        IEnumerable<EnterpriseNode> Nodes { get; }

        int NodeCount { get; }

        EnterpriseNode RootNode { get; }

        IEnumerable<EnterpriseUser> Users { get; }

        int UserCount { get; }

        IEnumerable<EnterpriseTeam> Teams { get; }

        int TeamCount { get; }

        bool TryGetNode(long nodeId, out EnterpriseNode node);

        bool TryGetUserById(long userId, out EnterpriseUser user);

        bool TryGetUserByEmail(string email, out EnterpriseUser user);

        bool TryGetTeam(string teamUid, out EnterpriseTeam team);
    }
}
