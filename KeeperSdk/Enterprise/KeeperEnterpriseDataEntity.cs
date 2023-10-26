using Enterprise;
using Google.Protobuf;
using System;
using System.Reflection;
using KeeperEnterpriseData = Enterprise.EnterpriseData;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public abstract class KeeperEnterpriseDataEntity<TK> : IKeeperEnterpriseEntity
    where TK : IMessage<TK>
    {
        private readonly MessageParser<TK> _parser;

        public EnterpriseDataEntity DataEntity { get; }

        public KeeperEnterpriseDataEntity(EnterpriseDataEntity dataEntity)
        {
            DataEntity = dataEntity;

            var keeperType = typeof(TK);
            var parser = keeperType.GetProperty("Parser", BindingFlags.Static | BindingFlags.Public);
            if (parser == null) throw new Exception($"Cannot get Parser for {keeperType.Name} Google Profobuf class");
            _parser = (MessageParser<TK>) (parser.GetMethod.Invoke(null, null));
        }

        protected TK Parse(ByteString data)
        {
            return _parser.ParseFrom(data);
        }

        protected virtual void DataStructureChanged() { }

        public abstract void ProcessKeeperEnterpriseData(KeeperEnterpriseData entityData);
        public abstract void Clear();
    }
}
