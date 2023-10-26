using System.Collections.Generic;
using System.Linq;

namespace KeeperSecurity.Vault
{
    internal class InMemorySentenceStorage<T> : IPredicateStorage<T> where T : IUidLink
    {
        private readonly Dictionary<string, IDictionary<string, T>> _links =
            new Dictionary<string, IDictionary<string, T>>();

        public void DeleteLinks(IEnumerable<IUidLink> links)
        {
            foreach (var link in links)
            {
                if (_links.TryGetValue(link.SubjectUid, out IDictionary<string, T> dict))
                {
                    dict.Remove(link.ObjectUid ?? "");
                }
            }
        }

        public void DeleteLinksForSubjects(IEnumerable<string> subjectUids)
        {
            foreach (var subjectUid in subjectUids)
            {
                _links.Remove(subjectUid ?? "");
            }
        }

        public void DeleteLinksForObjects(IEnumerable<string> objectUids)
        {
            foreach (var objectUid in objectUids)
            {
                foreach (var pair in _links)
                {
                    pair.Value?.Remove(objectUid ?? "");
                }
            }
        }

        public IEnumerable<T> GetAllLinks()
        {
            foreach (var dict in _links.Values)
            {
                foreach (var link in dict.Values)
                {
                    yield return link;
                }
            }
        }

        public IEnumerable<T> GetLinksForSubject(string primaryUid)
        {
            if (_links.TryGetValue(primaryUid, out IDictionary<string, T> dict))
            {
                return dict.Values;
            }

            return Enumerable.Empty<T>();
        }

        public IEnumerable<T> GetLinksForObject(string secondaryUid)
        {
            foreach (var dict in _links.Values)
            {
                if (dict.TryGetValue(secondaryUid, out T data))
                {
                    yield return data;
                }
            }
        }

        public void PutLinks(IEnumerable<T> links)
        {
            foreach (var link in links)
            {
                if (link == null)
                {
                    continue;
                }
                if (!_links.TryGetValue(link.SubjectUid, out IDictionary<string, T> dict))
                {
                    dict = new Dictionary<string, T>();
                    _links.Add(link.SubjectUid, dict);
                }

                var objectId = link.ObjectUid ?? "";
                if (dict.TryGetValue(objectId, out var elem))
                {
                    if (!ReferenceEquals(elem, link))
                    {
                        dict[objectId] = link;
                    }
                }
                else
                {
                    dict.Add(objectId, link);
                }
            }
        }
    }
}
