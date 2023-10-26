using Enterprise;
using KeeperSecurity.Utils;

namespace KeeperSecurity.Enterprise
{
    /// <exclude/>
    public class RoleTeamLink : EnterpriseDataLink<RoleTeam, RoleTeam, long, string>
    {
        public RoleTeamLink() : base(EnterpriseDataEntity.RoleTeams) { }

        protected override RoleTeam CreateFromKeeperEntity(RoleTeam keeperEntity)
        {
            return keeperEntity;
        }

        protected override long GetEntity1Id(RoleTeam keeperData)
        {
            return keeperData.RoleId;
        }

        protected override string GetEntity2Id(RoleTeam keeperData)
        {
            return keeperData.TeamUid.ToByteArray().Base64UrlEncode();
        }
    }
}
