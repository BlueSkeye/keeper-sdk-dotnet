using Enterprise;
using KeeperEnterpriseData = Enterprise.EnterpriseData;
using Google.Protobuf;
using System.Collections.Generic;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public abstract class EnterpriseDataList<TK, TS> : KeeperEnterpriseDataEntity<TK>
        where TK : IMessage<TK>
        where TS : class, new()
    {
        internal readonly List<TS> _entities = new List<TS>();

        protected abstract bool MatchByKeeperEntity(TS sdkEntity, TK keeperEntity);
        protected abstract TS CreateFromKeeperEntity(TK keeperEntity);

        public EnterpriseDataList(EnterpriseDataEntity dataEntity) : base(dataEntity)
        {
        }

        public override void Clear()
        {
            _entities.Clear();
        }

        public override void ProcessKeeperEnterpriseData(KeeperEnterpriseData entityData)
        {
            foreach (var data in entityData.Data)
            {
                var keeperEntity = Parse(data);
                if (entityData.Delete)
                {
                    lock (_entities)
                    {
                        _entities.RemoveAll((se) => MatchByKeeperEntity(se, keeperEntity));
                    }
                }
                else
                {
                    var se = CreateFromKeeperEntity(keeperEntity);
                    lock (_entities)
                    {
                        _entities.Add(se);
                    }
                }
            }
            DataStructureChanged();
        }

        public IEnumerable<TS> Entities => _entities;
    }
}
