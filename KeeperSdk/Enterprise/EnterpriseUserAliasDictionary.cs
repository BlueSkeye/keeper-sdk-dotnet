using Enterprise;
using KeeperEnterpriseData = Enterprise.EnterpriseData;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public class EnterpriseUserAliasDictionary : KeeperEnterpriseDataEntity<UserAlias>, IGetEnterprise
    {
        public Func<IEnterpriseLoader> GetEnterprise { get; set; }

        internal readonly ConcurrentDictionary<long, ISet<string>> _entities = new ConcurrentDictionary<long, ISet<string>>();

        public EnterpriseUserAliasDictionary() : base(EnterpriseDataEntity.UserAliases)
        {
        }

        public override void ProcessKeeperEnterpriseData(KeeperEnterpriseData entityData)
        {
            foreach (var data in entityData.Data)
            {
                var keeperEntity = Parse(data);
                var id = keeperEntity.EnterpriseUserId;
                if (!_entities.TryGetValue(id, out var sdkEntity))
                {
                    sdkEntity = new HashSet<string>();
                    _entities.TryAdd(id, sdkEntity);
                }

                if (entityData.Delete)
                {
                    sdkEntity.Remove(keeperEntity.Username);
                    if (sdkEntity.Count == 0)
                    {
                        _entities.TryRemove(id, out _);
                    }
                }
                else
                {
                    sdkEntity.Add(keeperEntity.Username);
                }
            }
            DataStructureChanged();
        }

        public bool TryGetEntity(long userId, out ISet<string> entity)
        {
            return _entities.TryGetValue(userId, out entity);
        }


        public override void Clear()
        {
            _entities.Clear();
        }

        public IEnumerable<long> UserIDs => _entities.Keys;

        public int Count => _entities.Count;

    }
}
