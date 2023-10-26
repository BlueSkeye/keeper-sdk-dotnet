
namespace KeeperSecurity.Enterprise
{
    /// <summary>
    /// Represends Managed Companies create/update options
    /// </summary>
    public class ManagedCompanyOptions
    {
        /// <summary>
        ///     Managed Company Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Enterprise Node ID
        /// </summary>
        public long? NodeId { get; set; }

        /// <summary>
        ///     Managed Company Product ID
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        ///     Maximum Number of Seats. -1 unlimited
        /// </summary>
        public int? NumberOfSeats { get; set; }

        /// <summary>
        ///     File/Storage Plan
        /// </summary>
        public string FilePlanType { get; set; }

        /// <summary>
        ///     Addons
        /// </summary>
        public ManagedCompanyAddonOptions[] Addons { get; set; }
    }
}
