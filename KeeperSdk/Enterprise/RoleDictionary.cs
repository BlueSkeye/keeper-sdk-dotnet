using Enterprise;
using System;

namespace KeeperSecurity.Enterprise
{
    /// <exclude/>
    public class RoleDictionary : EnterpriseDataDictionary<long, Role, EnterpriseRole>, IGetEnterprise
    {
        public RoleDictionary() : base(EnterpriseDataEntity.Roles)
        {
        }

        public Func<IEnterpriseLoader> GetEnterprise { get; set; }

        protected override long GetEntityId(Role keeperData)
        {
            return keeperData.RoleId;
        }

        protected override void PopulateSdkFromKeeper(EnterpriseRole sdk, Role keeper)
        {
            sdk.ParentNodeId = keeper.NodeId;
            sdk.EncryptedData = keeper.EncryptedData;
            sdk.KeyType = keeper.KeyType;
            sdk.NewUserInherit = keeper.NewUserInherit;
            sdk.VisibleBelow = keeper.VisibleBelow;
            sdk.RoleType = keeper.RoleType;
            var enterprise = GetEnterprise?.Invoke();
            if (enterprise != null && enterprise.TreeKey != null)
            {
                EnterpriseUtils.DecryptEncryptedData(keeper.EncryptedData, enterprise.TreeKey, sdk);
            }
        }

        protected override void SetEntityId(EnterpriseRole entity, long id)
        {
            entity.Id = id;
        }
    }
}
