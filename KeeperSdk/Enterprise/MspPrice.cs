using BI;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public class MspPrice
    {
        public float Amount { get; internal set; }
        public Cost.Types.AmountPer Rate { get; internal set; }
        public long AmountConsumed { get; internal set; }
        public Currency Currency { get; internal set; }
    }
}
