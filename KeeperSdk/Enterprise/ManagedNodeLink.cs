using Enterprise;

namespace KeeperSecurity.Enterprise
{
    /// <exclude/>
    public class ManagedNodeLink : EnterpriseDataLink<ManagedNode, ManagedNode, long, long>
    {
        public ManagedNodeLink() : base(EnterpriseDataEntity.ManagedNodes) { }

        protected override ManagedNode CreateFromKeeperEntity(ManagedNode keeperEntity)
        {
            return keeperEntity;
        }

        protected override long GetEntity1Id(ManagedNode keeperData)
        {
            return keeperData.RoleId;
        }

        protected override long GetEntity2Id(ManagedNode keeperData)
        {
            return keeperData.ManagedNodeId;
        }
    }
}
