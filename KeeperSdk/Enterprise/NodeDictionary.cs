using Enterprise;
using System;
using System.Linq;

namespace KeeperSecurity.Enterprise
{
    /// <exclude/>
    public class NodeDictionary : EnterpriseDataDictionary<long, Node, EnterpriseNode>, IGetEnterprise
    {
        public Func<IEnterpriseLoader> GetEnterprise { get; set; }

        public EnterpriseNode RootNode { get; private set; }

        public NodeDictionary() : base(EnterpriseDataEntity.Nodes)
        {
        }

        protected override long GetEntityId(Node keeperData)
        {
            return keeperData.NodeId;
        }

        protected override void SetEntityId(EnterpriseNode entity, long id)
        {
            entity.Id = id;
        }

        public override void Clear()
        {
            base.Clear();

            RootNode = null;
        }

        protected override void PopulateSdkFromKeeper(EnterpriseNode sdk, Node keeper)
        {
            sdk.ParentNodeId = keeper.ParentId;
            sdk.RestrictVisibility = keeper.RestrictVisibility;
            sdk.EncryptedData = keeper.EncryptedData;
            sdk.BridgeId = keeper.BridgeId;
            sdk.ScimId = keeper.ScimId;
            sdk.SsoServiceProviderIds = keeper.SsoServiceProviderIds.ToArray();
            var enterprise = GetEnterprise?.Invoke();
            if (enterprise?.TreeKey != null)
            {
                EnterpriseUtils.DecryptEncryptedData(keeper.EncryptedData, enterprise.TreeKey, sdk);
            }
        }

        protected override void DataStructureChanged()
        {
            foreach (var node in _entities.Values)
            {
                node.Subnodes.Clear();
                if (node.ParentNodeId == 0)
                {
                    RootNode = node;
                }
            }
            foreach (var node in _entities.Values)
            {
                if (_entities.TryGetValue(node.ParentNodeId, out var pNode))
                {
                    pNode.Subnodes.Add(node.Id);
                }
            }

            if (string.IsNullOrEmpty(RootNode?.DisplayName))
            {
                var enterprise = GetEnterprise?.Invoke();
                if (enterprise != null)
                {
                    RootNode.DisplayName = enterprise.EnterpriseName;
                }
            }
        }
    }
}
