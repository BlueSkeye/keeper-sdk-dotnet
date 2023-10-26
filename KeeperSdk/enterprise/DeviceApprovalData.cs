using Enterprise;
using System.Collections.Generic;

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    /// Represent Device Approval Queue data.
    /// </summary>
    public class DeviceApprovalData : EnterpriseDataPlugin
    {
        private readonly DeviceApprovalList _deviceApprovals;

        public DeviceApprovalData()
        {
            _deviceApprovals = new DeviceApprovalList();
            Entities = new IKeeperEnterpriseEntity[] { _deviceApprovals };
        }
        /// <exclude/>
        public override IEnumerable<IKeeperEnterpriseEntity> Entities { get; }

        /// <summary>
        /// Gets a list of all pending device approvals.
        /// </summary>
        public IEnumerable<DeviceRequestForAdminApproval> DeviceApprovalRequests => _deviceApprovals.Entities;
    }
}
