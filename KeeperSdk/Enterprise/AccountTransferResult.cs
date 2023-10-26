
namespace KeeperSecurity.Enterprise
{
    public class AccountTransferResult
    {
        public int RecordsTransfered { get; internal set; }

        public int SharedFoldersTransfered { get; internal set; }

        public int TeamsTransfered { get; internal set; }

        public int UserFoldersTransfered { get; internal set; }

        public int RecordsCorrupted { get; internal set; }

        public int SharedFoldersCorrupted { get; internal set; }

        public int TeamsCorrupted { get; internal set; }

        public int UserFoldersCorrupted { get; internal set; }
    };
}
