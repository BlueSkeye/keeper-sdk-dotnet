using System.Collections.Generic;

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    ///     Defines Queued Team storage
    /// </summary>
    public interface IQueuedTeamData
    {
        IEnumerable<EnterpriseQueuedTeam> QueuedTeams { get; }
        IEnumerable<long> GetQueuedUsersForTeam(string teamUid);
    }
}
