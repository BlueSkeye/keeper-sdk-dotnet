using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    internal class RebuildTask
    {
        internal RebuildTask(bool isFullSync)
        {
            IsFullSync = isFullSync;
        }

        public bool IsFullSync { get; }

        public void AddRecord(string recordUid)
        {
            if (IsFullSync) return;
            if (Records == null)
            {
                Records = new HashSet<string>();
            }

            Records.Add(recordUid);
        }

        public void AddRecords(IEnumerable<string> recordUids)
        {
            foreach (var recordUid in recordUids)
            {
                AddRecord(recordUid);
            }
        }

        public void AddSharedFolder(string sharedFolderUid)
        {
            if (IsFullSync) return;
            if (SharedFolders == null)
            {
                SharedFolders = new HashSet<string>();
            }

            SharedFolders.Add(sharedFolderUid);
        }

        public void AddSharedFolders(IEnumerable<string> sharedFolderUids)
        {
            foreach (var sharedFolderUid in sharedFolderUids)
            {
                AddSharedFolder(sharedFolderUid);
            }
        }

        public ISet<string> Records { get; private set; }
        public ISet<string> SharedFolders { get; private set; }
    }
}
