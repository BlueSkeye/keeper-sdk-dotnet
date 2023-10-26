using System.Threading.Tasks;

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    /// Defines Managed Company actions
    /// </summary>
    public interface IMspManagement
    {
        /// <summary>
        /// Creates Managed Company
        /// </summary>
        /// <param name="options">Company options</param>
        /// <returns>Created managed company</returns>
        Task<EnterpriseManagedCompany> CreateManagedCompany(ManagedCompanyOptions options);
        /// <summary>
        /// Updates Managed Company
        /// </summary>
        /// <param name="companyId">Managed Company ID</param>
        /// <param name="options">Company options</param>
        /// <returns>Updated managed company</returns>
        Task<EnterpriseManagedCompany> UpdateManagedCompany(int companyId, ManagedCompanyOptions options);
        /// <summary>
        /// Removes Managed Company
        /// </summary>
        /// <param name="companyId">Managed Company ID</param>
        /// <returns></returns>
        Task RemoveManagedCompany(int companyId);
    }
}
