using System.Threading.Tasks;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines methods for interaction between Vault API and user.
    /// </summary>
    public interface IVaultUi
    {
        /// <summary>
        /// Ask confirmation from user.
        /// </summary>
        /// <param name="information">text to be displayed in the dialog.</param>
        /// <returns>Task returning <c>bool</c>; <c>true</c> means Yes/Accept; <c>false</c> No/Decline</returns>
        /// <seealso cref="IVault.DeleteRecords"/>
        /// <seealso cref="IVault.DeleteFolder"/>
        Task<bool> Confirmation(string information);
    }
}
