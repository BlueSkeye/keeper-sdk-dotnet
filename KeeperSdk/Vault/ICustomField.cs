
namespace KeeperSecurity.Vault
{
    /// <exclude />
    public interface ICustomField
    {
        string Type { get; }
        string Name { get; }
        string Value { get; set; }
    }
}
