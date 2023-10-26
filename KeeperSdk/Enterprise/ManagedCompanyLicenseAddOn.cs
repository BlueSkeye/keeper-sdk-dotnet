
namespace KeeperSecurity.Enterprise
{
    /// <summary>
    ///     Represends Managed Company Add-On
    /// </summary>
    public class ManagedCompanyLicenseAddOn
    {
        /// <summary>
        ///     Add-On name
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     Is Add-On enabled
        /// </summary>
        public bool IsEnabled { get; internal set; }

        /// <summary>
        ///     Is Add-On trial
        /// </summary>
        public bool IsTrial { get; internal set; }

        /// <summary>
        /// Number of Seats
        /// </summary>
        public int Seats { get; internal set; }

        /// <summary>
        /// Add-On expiration time. UNIX epoch
        /// </summary>
        public long Expiration { get; internal set; }

        /// <summary>
        /// Add-On creation time. UNIX epoch
        /// </summary>
        public long Creation { get; internal set; }

        /// <summary>
        /// Add-On activation time. UNIX epoch
        /// </summary>
        public long Activation { get; internal set; }
    }
}
