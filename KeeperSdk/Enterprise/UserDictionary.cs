using Enterprise;
using System;
using System.Collections.Concurrent;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public class UserDictionary : EnterpriseDataDictionary<long, User, EnterpriseUser>, IGetEnterprise
    {
        public Func<IEnterpriseLoader> GetEnterprise { get; set; }

        private readonly ConcurrentDictionary<string, long> _userNames = new ConcurrentDictionary<string, long>(1, 100, StringComparer.InvariantCultureIgnoreCase);

        public UserDictionary() : base(EnterpriseDataEntity.Users)
        {
        }

        protected override long GetEntityId(User keeperData)
        {
            return keeperData.EnterpriseUserId;
        }

        protected override void SetEntityId(EnterpriseUser entity, long id)
        {
            entity.Id = id;
        }

        public override void Clear()
        {
            base.Clear();

            _userNames.Clear();
        }

        protected override void PopulateSdkFromKeeper(EnterpriseUser sdk, User keeper)
        {
            sdk.ParentNodeId = keeper.NodeId;
            sdk.EncryptedData = keeper.EncryptedData;
            sdk.Email = keeper.Username;
            if (keeper.Status == "active")
            {
                switch (keeper.Lock)
                {
                    case 0:
                        sdk.UserStatus = UserStatus.Active;
                        break;
                    case 1:
                        sdk.UserStatus = UserStatus.Locked;
                        break;
                    case 2:
                        sdk.UserStatus = UserStatus.Disabled;
                        break;
                    default:
                        sdk.UserStatus = UserStatus.Active;
                        break;
                }

                if (keeper.AccountShareExpiration > 0)
                {
                    var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    if (now > keeper.AccountShareExpiration)
                    {
                        sdk.UserStatus = UserStatus.Blocked;
                    }
                }
            }
            else
            {
                sdk.UserStatus = UserStatus.Inactive;
            }

            var enterprise = GetEnterprise?.Invoke();
            if (enterprise != null && enterprise.TreeKey != null)
            {
                EnterpriseUtils.DecryptEncryptedData(keeper.EncryptedData, enterprise.TreeKey, sdk);
            }
        }

        public bool TryGetUserByEmail(string email, out EnterpriseUser user)
        {
            user = null;
            if (_userNames.TryGetValue(email, out var id))
            {
                return _entities.TryGetValue(id, out user);
            }

            return false;
        }

        protected override void DataStructureChanged()
        {
            _userNames.Clear();
            foreach (var user in _entities.Values)
            {
                _userNames.TryAdd(user.Email, user.Id);
            }
        }
    }
}
