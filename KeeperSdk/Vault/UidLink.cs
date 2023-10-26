using System;

namespace KeeperSecurity.Vault
{
    internal class UidLink : Tuple<string, string>, IUidLink
    {
        private UidLink(string objectUid, string subjectUid) : base(objectUid, subjectUid ?? "")
        {
        }

        public static UidLink Create(string objectUid, string subjectUid)
        {
            return new UidLink(objectUid, subjectUid);
        }

        string IUidLink.SubjectUid => Item1;
        string IUidLink.ObjectUid => Item2;
    }
}
