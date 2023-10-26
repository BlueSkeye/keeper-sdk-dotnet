using Enterprise;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using KeeperEnterpriseData = Enterprise.EnterpriseData;
using KeeperSecurity.Utils;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public class QueuedUserDictionary : KeeperEnterpriseDataEntity<QueuedTeamUser>, IGetEnterprise
    {
        public Func<IEnterpriseLoader> GetEnterprise { get; set; }

        internal readonly ConcurrentDictionary<string, ISet<long>> _entities = new ConcurrentDictionary<string, ISet<long>>();

        public QueuedUserDictionary() : base(EnterpriseDataEntity.QueuedTeamUsers)
        {
        }

        public override void ProcessKeeperEnterpriseData(KeeperEnterpriseData entityData)
        {
            foreach (var data in entityData.Data)
            {
                var keeperEntity = Parse(data);
                var id = keeperEntity.TeamUid.ToByteArray().Base64UrlEncode();
                if (!_entities.TryGetValue(id, out var sdkEntity))
                {
                    sdkEntity = new HashSet<long>();
                    _entities.TryAdd(id, sdkEntity);
                }

                foreach (var userId in keeperEntity.Users)
                {
                    if (entityData.Delete)
                    {
                        sdkEntity.Remove(userId);
                    }
                    else
                    {
                        sdkEntity.Add(userId);
                    }
                }

                if (sdkEntity.Count == 0)
                {
                    _entities.TryRemove(id, out _);
                }
            }
            DataStructureChanged();
        }

        public bool TryGetMembers(string key, out ISet<long> entity)
        {
            return _entities.TryGetValue(key, out entity);
        }


        public override void Clear()
        {
            _entities.Clear();
        }
    }
}
