using Enterprise;

namespace KeeperSecurity.Enterprise
{
    /// <exclude/>
    public class RoleUserLink : EnterpriseDataLink<RoleUser, RoleUser, long, long>
    {
        public RoleUserLink() : base(EnterpriseDataEntity.RoleUsers) { }

        protected override RoleUser CreateFromKeeperEntity(RoleUser keeperEntity)
        {
            return keeperEntity;
        }

        protected override long GetEntity1Id(RoleUser keeperData)
        {
            return keeperData.RoleId;
        }

        protected override long GetEntity2Id(RoleUser keeperData)
        {
            return keeperData.EnterpriseUserId;
        }
    }
}
