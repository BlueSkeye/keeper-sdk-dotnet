using System.Collections.Generic;
using System.Linq;

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    /// Represents Queued Team Enterprise Plugin
    /// </summary>
    public class QueuedTeamData : EnterpriseDataPlugin, IQueuedTeamData
    {
        private readonly QueuedTeamDictionary _queuedTeams;
        private readonly QueuedUserDictionary _queuedUsers;

        /// <summary>
        /// Instantiates <see cref="QueuedTeamData"/> instance.
        /// </summary>
        public QueuedTeamData() : base()
        {
            _queuedTeams = new QueuedTeamDictionary();
            _queuedUsers = new QueuedUserDictionary();

            Entities = new IKeeperEnterpriseEntity[] { _queuedTeams, _queuedUsers };
        }

        /// <exclude />
        public override IEnumerable<IKeeperEnterpriseEntity> Entities { get; }

        /// <summary>
        /// Gets list of all queued teams
        /// </summary>
        public IEnumerable<EnterpriseQueuedTeam> QueuedTeams => _queuedTeams.Entities;
        /// <summary>
        /// Gets the number of all queued teams in the enterprise.
        /// </summary>
        public int QueuedTeamCount => _queuedTeams.Count;

        /// <summary>
        /// Gets Gets a list of user IDs for specified queued team.
        /// </summary>
        /// <param name="teamUid">Queued Team UID</param>
        /// <returns>A list of user IDs</returns>
        public IEnumerable<long> GetQueuedUsersForTeam(string teamUid)
        {
            if (_queuedUsers.TryGetMembers(teamUid, out var users))
            {
                return users;
            }
            return Enumerable.Empty<long>();
        }
    }
}
