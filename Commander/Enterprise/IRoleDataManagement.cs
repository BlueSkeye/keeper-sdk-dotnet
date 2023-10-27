using KeeperSecurity.Enterprise;
using System.Threading.Tasks;

namespace Commander.Enterprise
{
    public interface IRoleDataManagement : IRoleData
    {
        Task<EnterpriseRole> CreateRole(string roleName, long nodeId, bool visibleBelow, bool newUserInherit);
        Task DeleteRole(long roleId);

        Task AddUserToRole(long roleId, long userId);
        Task AddUserToAdminRole(long roleId, long userId, byte[] userRsaPublicKey);
        Task RemoveUserFromRole(long roleId, long userId);
        Task AddTeamToRole(long roleId, string teamUid);
        Task RemoveTeamFromRole(long roleId, string teamUid);
    }
}
