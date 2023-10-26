using Enterprise;
using KeeperEnterpriseData = Enterprise.EnterpriseData;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public interface IKeeperEnterpriseEntity
    {
        EnterpriseDataEntity DataEntity { get; }
        void ProcessKeeperEnterpriseData(KeeperEnterpriseData entityData);
        void Clear();
    }
}
