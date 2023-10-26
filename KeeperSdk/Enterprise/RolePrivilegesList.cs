using Enterprise;

namespace KeeperSecurity.Enterprise
{
    /// <exclude/>
    public class RolePrivilegesList : EnterpriseDataList<RolePrivilege, RolePrivilege>
    {
        public RolePrivilegesList() : base(EnterpriseDataEntity.RolePrivileges) { }

        protected override RolePrivilege CreateFromKeeperEntity(RolePrivilege keeperEntity)
        {
            return keeperEntity;
        }

        protected override bool MatchByKeeperEntity(RolePrivilege sdkEntity, RolePrivilege keeperEntity)
        {
            return
                sdkEntity.RoleId == keeperEntity.RoleId &&
                sdkEntity.ManagedNodeId == keeperEntity.ManagedNodeId &&
                string.Equals(sdkEntity.PrivilegeType, keeperEntity.PrivilegeType);
        }
    }
}
