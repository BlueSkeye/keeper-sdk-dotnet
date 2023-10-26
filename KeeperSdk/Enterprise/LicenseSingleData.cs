using Enterprise;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public class LicenseSingleData : EnterpriseSingleData<License, License>
    {
        public LicenseSingleData() : base(EnterpriseDataEntity.Licenses) { }
        protected override License GetSdkFromKeeper(License keeper)
        {
            return keeper;
        }
    }
}
