using KeeperSecurity.Vault;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    public interface IEntityLink : IUidLink
    {
        new string SubjectUid { get; set; }
        new string ObjectUid { get; set; }
    }
}
