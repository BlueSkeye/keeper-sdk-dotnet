using Enterprise;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using KeeperEnterpriseData = Enterprise.EnterpriseData;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public class UserAliasData : EnterpriseDataPlugin, IUserAliasData
    {
        private readonly EnterpriseUserAliasDictionary _aliases = new EnterpriseUserAliasDictionary();

        public UserAliasData()
        {
            Entities = new[] { _aliases };
        }
        public IEnumerable<string> GetAliasesForUser(long userId)
        {
            if (_aliases.TryGetEntity(userId, out var entity))
            {
                return entity;
            }
            return Enumerable.Empty<string>();
        }

        public override IEnumerable<IKeeperEnterpriseEntity> Entities { get; }
    }
}
