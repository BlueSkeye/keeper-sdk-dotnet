using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines entity storage methods.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    public interface IEntityStorage<T> where T : IUid
    {
        /// <summary>
        /// Gets entity by entity UID.
        /// </summary>
        /// <param name="uid">Entity UID.</param>
        /// <returns>Entity instance.</returns>
        T GetEntity(string uid);
        /// <summary>
        /// Stores entities.
        /// </summary>
        /// <param name="entities">List of entities.</param>
        void PutEntities(IEnumerable<T> entities);
        /// <summary>
        /// Deletes entity by entity UID.
        /// </summary>
        /// <param name="uids">List of Entity UIDs to delete.</param>
        void DeleteUids(IEnumerable<string> uids);
        /// <summary>
        /// Gets all entities in the storage.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
    }
}
