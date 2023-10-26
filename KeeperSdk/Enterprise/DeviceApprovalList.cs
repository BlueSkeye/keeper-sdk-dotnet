using Enterprise;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public class DeviceApprovalList : EnterpriseDataList<DeviceRequestForAdminApproval, DeviceRequestForAdminApproval>
    {
        public DeviceApprovalList()
            : base(EnterpriseDataEntity.DevicesRequestForAdminApproval)
        {
        }

        protected override DeviceRequestForAdminApproval CreateFromKeeperEntity(DeviceRequestForAdminApproval keeperEntity)
        {
            return keeperEntity;
        }

        protected override bool MatchByKeeperEntity(DeviceRequestForAdminApproval sdkEntity, DeviceRequestForAdminApproval keeperEntity)
        {
            return sdkEntity.EnterpriseUserId == keeperEntity.EnterpriseUserId && sdkEntity.DeviceId == keeperEntity.DeviceId;
        }
    }
}
