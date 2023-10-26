using KeeperSecurity.Commands;

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    ///     Represents Queued Team
    /// </summary>
    public class EnterpriseQueuedTeam : IParentNodeEntity, IEncryptedData
    {
        public string Uid { get; internal set; }
        public string Name { get; set; }
        public long ParentNodeId { get; internal set; }

        public string EncryptedData { get; internal set; }
    }
}
