using KeeperSecurity.Vault;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    public interface IEntity : IUid
    {
        new string Uid { get; set; }
    }
}
