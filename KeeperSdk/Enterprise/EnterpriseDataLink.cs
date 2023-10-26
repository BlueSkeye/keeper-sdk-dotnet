using Enterprise;
using KeeperEnterpriseData = Enterprise.EnterpriseData;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public abstract class EnterpriseDataLink<TK, TS, TD1, TD2> : KeeperEnterpriseDataEntity<TK>
        where TK : IMessage<TK>
        where TD1 : IComparable<TD1>
        where TD2 : IComparable<TD2>
    {

        protected readonly List<TS> _links = new List<TS>();

        public EnterpriseDataLink(EnterpriseDataEntity dataEntity) : base(dataEntity)
        {
        }

        protected abstract TD1 GetEntity1Id(TS keeperData);
        protected abstract TD2 GetEntity2Id(TS keeperData);
        protected abstract TS CreateFromKeeperEntity(TK keeperEntity);

        public override void Clear()
        {
            _links.Clear();
        }

        public IList<TS> LinksForPrimaryKey(TD1 primaryId)
        {
            lock (_links)
            {
                return _links.Where(x => GetEntity1Id(x).CompareTo(primaryId) == 0).ToList();
            }
        }

        public IList<TS> LinksForSecondaryKey(TD2 secondaryId)
        {
            lock (_links)
            {
                return _links.Where(x => GetEntity2Id(x).CompareTo(secondaryId) == 0).ToList();
            }
        }

        public IList<TS> GetAllLinks()
        {
            lock (_links)
            {
                return _links.ToArray();
            }
        }

        public override void ProcessKeeperEnterpriseData(KeeperEnterpriseData entityData)
        {
            foreach (var data in entityData.Data)
            {
                var keeperEntity = Parse(data);
                var sdkEntity = CreateFromKeeperEntity(keeperEntity);
                if (entityData.Delete)
                {
                    lock (_links)
                    {
                        _links.RemoveAll(x => GetEntity1Id(x).CompareTo(GetEntity1Id(sdkEntity)) == 0 && GetEntity2Id(x).CompareTo(GetEntity2Id(sdkEntity)) == 0);
                    }
                }
                else
                {
                    lock (_links)
                    {
                        _links.Add(sdkEntity);
                    }
                }
            }

            DataStructureChanged();
        }
    }
}
