
namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines properties for Enterprise Team.
    /// </summary>
    public interface IEnterpriseTeam : IUid
    {
        /// <summary>
        /// Team UID.
        /// </summary>
        string TeamUid { get; }
        /// <summary>
        /// Team name. Plain text.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Team key. Encrypted with the <see cref="KeyType"/>
        /// </summary>
        string TeamKey { get; }
        /// <summary>
        /// Encryption key type.
        /// </summary>
        /// <see cref="Vault.KeyType"/>
        int KeyType { get; }
        /// <summary>
        /// Team private key. Encrypted with the team key.
        /// </summary>
        string TeamPrivateKey { get; }
        /// <summary>
        /// Does team restrict record edit?
        /// </summary>
        bool RestrictEdit { get; }
        /// <summary>
        /// Does team restrict record re-share?
        /// </summary>
        bool RestrictShare { get; }
        /// <summary>
        /// Does team restrict record view?
        /// </summary>
        bool RestrictView { get; }
    }
}
