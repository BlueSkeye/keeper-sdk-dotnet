using KeeperSecurity.Authentication;
using System.Threading.Tasks;

namespace KeeperSecurity.Enterprise
{
    /// <exclude/>
    public interface IEnterpriseLoader
    {
        IAuthentication Auth { get; }
        string EnterpriseName { get; }
        byte[] TreeKey { get; }
        Task Load();
        Task<long> GetEnterpriseId();
    }
}
