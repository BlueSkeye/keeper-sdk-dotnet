using System;
using System.Threading.Tasks;

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    /// Defines methods for modifying enterprise users and teams. 
    /// </summary>
    /// <seealso cref="VaultOnline"/>
    public interface IEnterpriseDataManagement
    {
        /// <summary>
        ///     Invides User to Enterprise.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="options">Invided user options</param>
        /// <returns>Invited User</returns>
        Task<EnterpriseUser> InviteUser(string email, InviteUserOptions options = null);
        /// <summary>
        ///     Locks or Unlocks Enterprise User.
        /// </summary>
        /// <param name="user">Enterprise User</param>
        /// <param name="locked">Lock flag</param>
        /// <returns>User</returns>
        Task<EnterpriseUser> SetUserLocked(EnterpriseUser user, bool locked);
        /// <summary>
        ///     Deletes Enterprise User.
        /// </summary>
        /// <param name="user">Enterprise User</param>
        /// <returns>Task</returns>
        Task DeleteUser(EnterpriseUser user);
        /// <summary>
        ///     Transfers Enterprise User account to another user.
        /// </summary>
        /// <param name="roleData">Enterprise Role data</param>
        /// <param name="fromUser">Enterprise User to transfer account from</param>
        /// <param name="targetUser">Target Enterprise User</param>
        /// <returns>Task</returns>
        Task<AccountTransferResult> TransferUserAccount(IRoleData roleData, EnterpriseUser fromUser, EnterpriseUser targetUser);

        /// <summary>
        ///     Creates Enterprise Team.
        /// </summary>
        /// <param name="team">Enterprise Team</param>
        /// <returns>Created Team</returns>
        Task<EnterpriseTeam> CreateTeam(EnterpriseTeam team);
        /// <summary>
        ///     Modifies Enterprise Team.
        /// </summary>
        /// <param name="team">Enterprise Team</param>
        /// <returns>Updated Team</returns>
        Task<EnterpriseTeam> UpdateTeam(EnterpriseTeam team);
        /// <summary>
        ///     Deletes Enterprise Team.
        /// </summary>
        /// <param name="team">Enterprise Team</param>
        /// <returns>Task</returns>
        Task DeleteTeam(string teamUid);
        /// <summary>
        ///     Adds Users to Team.
        /// </summary>
        /// <param name="teamUid">Team Uid</param>
        /// <returns>Task</returns>
        Task AddUsersToTeams(string[] emails, string[] teamUids, Action<string> warnings = null);
        /// <summary>
        ///     Removes Users(s) from Team(s)
        /// </summary>
        /// <param name="emails">A list of user emails</param>
        /// <param name="teamUids">A list of team UIDs</param>
        /// <param name="warnings">A callback that receives warnings</param>
        /// <returns>Awaitable task.</returns>
        Task RemoveUsersFromTeams(string[] emails, string[] teamUids, Action<string> warnings = null);
    }
}
