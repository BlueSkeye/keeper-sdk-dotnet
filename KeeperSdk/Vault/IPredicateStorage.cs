using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines entity link storage methods.
    /// </summary>
    /// <typeparam name="T">Type of entity link.</typeparam>
    public interface IPredicateStorage<T> where T : IUidLink
    {
        /// <summary>
        /// Stores entity links
        /// </summary>
        /// <param name="entities">List of entity links.</param>
        void PutLinks(IEnumerable<T> entities);
        /// <summary>
        /// Deletes entity link.
        /// </summary>
        /// <param name="links">List links to delete.</param>
        void DeleteLinks(IEnumerable<IUidLink> links);
        /// <summary>
        /// Delete all links for subject entity UIDs
        /// </summary>
        /// <param name="subjectUids">List of Subject UIDs to delete.</param>
        void DeleteLinksForSubjects(IEnumerable<string> subjectUids);
        /// <summary>
        /// Delete all links for object entity UID
        /// </summary>
        /// <param name="objectUid">List of Object UIDs to delete.</param>
        void DeleteLinksForObjects(IEnumerable<string> objectUids);
        /// <summary>
        /// Gets all entity links for subject entity UID.
        /// </summary>
        /// <param name="subjectUid">Subject UID.</param>
        /// <returns>A list of entity links.</returns>
        IEnumerable<T> GetLinksForSubject(string subjectUid);
        /// <summary>
        /// Gets all entity links for object entity UID.
        /// </summary>
        /// <param name="objectUid">Object UID.</param>
        /// <returns>A list of entity links.</returns>
        IEnumerable<T> GetLinksForObject(string objectUid);
        /// <summary>
        /// Gets all entity links in the storage.
        /// </summary>
        /// <returns>A list of entity links.</returns>
        IEnumerable<T> GetAllLinks();
    }
}
