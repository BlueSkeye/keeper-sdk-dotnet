using Enterprise;
using KeeperSecurity.Utils;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public class TeamUserDataLink : EnterpriseDataLink<TeamUser, TeamUser, string, long>
    {
        public TeamUserDataLink() : base(EnterpriseDataEntity.TeamUsers)
        {
        }

        protected override TeamUser CreateFromKeeperEntity(TeamUser keeperEntity)
        {
            return keeperEntity;
        }

        protected override string GetEntity1Id(TeamUser keeperData)
        {
            return keeperData.TeamUid.ToByteArray().Base64UrlEncode();
        }
        protected override long GetEntity2Id(TeamUser keeperData)
        {
            return keeperData.EnterpriseUserId;
        }
    }
}
