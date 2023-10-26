using Enterprise;
using KeeperSecurity.Utils;
using System;
using System.Diagnostics;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public class TeamDictionary : EnterpriseDataDictionary<string, Team, EnterpriseTeam>, IGetEnterprise
    {
        public Func<IEnterpriseLoader> GetEnterprise { get; set; }

        public TeamDictionary() : base(EnterpriseDataEntity.Teams)
        {
        }

        protected override string GetEntityId(Team keeperData)
        {
            return keeperData.TeamUid.ToByteArray().Base64UrlEncode();
        }

        protected override void SetEntityId(EnterpriseTeam entity, string uid)
        {
            entity.Uid = uid;
        }

        protected override void PopulateSdkFromKeeper(EnterpriseTeam sdk, Team keeper)
        {
            sdk.ParentNodeId = keeper.NodeId;
            sdk.RestrictEdit = keeper.RestrictEdit;
            sdk.RestrictSharing = keeper.RestrictShare;
            sdk.RestrictView = keeper.RestrictView;
            sdk.Name = keeper.Name;

            if (!string.IsNullOrEmpty(keeper.EncryptedTeamKey))
            {
                var enterprise = GetEnterprise?.Invoke();
                if (enterprise != null && enterprise.TreeKey != null)
                {
                    try
                    {
                        sdk.TeamKey = CryptoUtils.DecryptAesV2(keeper.EncryptedTeamKey.Base64UrlDecode(), enterprise.TreeKey);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
