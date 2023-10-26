using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    internal class InMemoryItemStorage<T> : IEntityStorage<T> where T : IUid
    {
        private readonly Dictionary<string, T> _items = new Dictionary<string, T>();

        public void DeleteUids(IEnumerable<string> uids)
        {
            foreach (var uid in uids)
            {
                _items.Remove(uid);
            }
        }

        public T GetEntity(string uid)
        {
            if (_items.TryGetValue(uid, out T item))
            {
                return item;
            }

            return default;
        }

        public IEnumerable<T> GetAll()
        {
            return _items.Values;
        }

        public void PutEntities(IEnumerable<T> data)
        {
            foreach (var entity in data)
            {
                if (entity != null)
                {
                    _items[entity.Uid] = entity;
                }
            }
        }
    }
}
