using Enterprise;

namespace KeeperSecurity.Enterprise
{
    /// <exclude/>
    public class RoleEnforcementLink : EnterpriseDataLink<RoleEnforcement, RoleEnforcement, long, string>
    {
        public RoleEnforcementLink() : base(EnterpriseDataEntity.RoleEnforcements) { }

        protected override RoleEnforcement CreateFromKeeperEntity(RoleEnforcement keeperEntity)
        {
            return keeperEntity;
        }

        protected override long GetEntity1Id(RoleEnforcement keeperData)
        {
            return keeperData.RoleId;
        }

        protected override string GetEntity2Id(RoleEnforcement keeperData)
        {
            return keeperData.EnforcementType;
        }
    }
}
