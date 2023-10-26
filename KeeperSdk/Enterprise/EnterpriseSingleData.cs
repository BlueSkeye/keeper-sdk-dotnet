using Enterprise;
using KeeperEnterpriseData = Enterprise.EnterpriseData;
using Google.Protobuf;
using System.Linq;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public abstract class EnterpriseSingleData<TK, TS> : KeeperEnterpriseDataEntity<TK>
        where TK : IMessage<TK>
        where TS : class
    {
        public EnterpriseSingleData(EnterpriseDataEntity dataEntity) : base(dataEntity)
        {
        }

        public TS Entity { get; protected set; }

        protected abstract TS GetSdkFromKeeper(TK keeper);

        public override void ProcessKeeperEnterpriseData(KeeperEnterpriseData entityData)
        {
            if (entityData.Delete)
            {
                Entity = null;
            }
            else
            {
                var data = entityData.Data.LastOrDefault();
                if (data != null)
                {

                    Entity = GetSdkFromKeeper(Parse(data));
                }
            }
        }
        public override void Clear()
        {
            Entity = null;
        }
    }
}
