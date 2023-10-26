using Enterprise;
using KeeperEnterpriseData = Enterprise.EnterpriseData;
using Google.Protobuf;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public abstract class EnterpriseDataDictionary<TD, TK, TS> : KeeperEnterpriseDataEntity<TK>
        where TK : IMessage<TK>
        where TS : class, new()
    {
        internal readonly ConcurrentDictionary<TD, TS> _entities = new ConcurrentDictionary<TD, TS>();

        public EnterpriseDataDictionary(EnterpriseDataEntity dataEntity) : base(dataEntity)
        {
        }

        protected abstract void PopulateSdkFromKeeper(TS sdk, TK keeper);
        protected abstract void SetEntityId(TS entity, TD id);

        protected abstract TD GetEntityId(TK keeperData);

        public override void Clear()
        {
            _entities.Clear();
        }

        public bool TryGetEntity(TD key, out TS entity)
        {
            return _entities.TryGetValue(key, out entity);
        }

        public IEnumerable<TS> Entities => _entities.Values;

        public int Count => _entities.Count;

        public override void ProcessKeeperEnterpriseData(KeeperEnterpriseData entityData)
        {
            foreach (var data in entityData.Data)
            {
                var keeperEntity = Parse(data);
                var id = GetEntityId(keeperEntity);
                if (entityData.Delete)
                {
                    _entities.TryRemove(id, out _);
                }
                else
                {
                    if (!_entities.TryGetValue(id, out var sdkEntity))
                    {
                        sdkEntity = new TS();
                        SetEntityId(sdkEntity, id);
                        _entities.TryAdd(id, sdkEntity);
                    }

                    PopulateSdkFromKeeper(sdkEntity, keeperEntity);
                }
            }
            DataStructureChanged();
        }
    }
}
