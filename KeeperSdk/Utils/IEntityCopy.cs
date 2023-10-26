
namespace KeeperSecurity.Utils
{
    /// <exclude/>
    public interface IEntityCopy<in T>
    {
        void CopyFields(T source);
    }
}
