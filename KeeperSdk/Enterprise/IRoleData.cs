using Enterprise;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    /// Defines Role enterprise data.
    /// </summary>
    public interface IRoleData
    {
        /// <summary>
        /// Get a list of all roles in the enterprise
        /// </summary>
        IEnumerable<EnterpriseRole> Roles { get; }
        /// <summary>
        /// Gets the number of all roles in the enterprise.
        /// </summary>
        int RoleCount { get; }
        /// <summary>
        /// Gets the enterprise role assocoated with the specified ID.
        /// </summary>
        /// <param name="roleId">Enterprise Role ID</param>
        /// <param name="role">When this method returns <c>true</c>, contains requested enterprise role; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if the enterprise contains a role with specified ID; otherwise, <c>false</c></returns>
        bool TryGetRole(long roleId, out EnterpriseRole role);
        /// <summary>
        /// Gets a list of user IDs for specified role.
        /// </summary>
        /// <param name="roleId">Enterprise Role ID.</param>
        /// <returns>List of Enterprise User IDs.</returns>
        IEnumerable<long> GetUsersForRole(long roleId);
        /// <summary>
        /// Gets a list of role IDs for specified user.
        /// </summary>
        /// <param name="userId">Enterprise User ID.</param>
        /// <returns>List of Enterprise Role IDs</returns>
        IEnumerable<long> GetRolesForUser(long userId);
        /// <summary>
        /// Gets a list of team UIDs for specified role.
        /// </summary>
        /// <param name="roleId">Enterprise Role ID.</param>
        /// <returns>List of Enterprise Team UIDs.</returns>
        IEnumerable<string> GetTeamsForRole(long roleId);
        /// <summary>
        /// Gets a list of role IDs for specified team.
        /// </summary>
        /// <param name="teamUid">Team UID.</param>
        /// <returns>List of Enterprise Role IDs</returns>
        IEnumerable<long> GetRolesForTeam(string teamUid);
        /// <summary>
        /// Gets a list of role enforcements for specified role.
        /// </summary>
        /// <param name="roleId">Enterprise Role ID.</param>
        /// <returns>List of Role Enforcements</returns>
        IEnumerable<RoleEnforcement> GetEnforcementsForRole(long roleId);

        /// <summary>
        /// Gets role key.
        /// </summary>
        /// <param name="roleId">Enterprise Role ID.</param>
        /// <returns>Role Key</returns>
        Task<byte[]> GetRoleKey(long roleId);
    }
}
