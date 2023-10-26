using Enterprise;
using System;
using KeeperSecurity.Utils;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public class QueuedTeamDictionary : EnterpriseDataDictionary<string, QueuedTeam, EnterpriseQueuedTeam>, IGetEnterprise
    {
        public Func<IEnterpriseLoader> GetEnterprise { get; set; }

        public QueuedTeamDictionary() : base(EnterpriseDataEntity.QueuedTeams)
        {
        }

        protected override string GetEntityId(QueuedTeam keeperData)
        {
            return keeperData.TeamUid.ToByteArray().Base64UrlEncode();
        }

        protected override void SetEntityId(EnterpriseQueuedTeam entity, string uid)
        {
            entity.Uid = uid;
        }

        protected override void PopulateSdkFromKeeper(EnterpriseQueuedTeam sdk, QueuedTeam keeper)
        {
            sdk.Name = keeper.Name;
            sdk.ParentNodeId = keeper.NodeId;
            sdk.EncryptedData = keeper.EncryptedData;
        }
    }
}
