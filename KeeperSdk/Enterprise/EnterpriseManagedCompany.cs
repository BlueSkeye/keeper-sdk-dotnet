
namespace KeeperSecurity.Enterprise
{
    /// <summary>
    ///     Represents Enterprise Managed Company.
    /// </summary>
    public class EnterpriseManagedCompany : IParentNodeEntity
    {
        /// <summary>
        ///     Managed Company Enterprise ID
        /// </summary>
        public int EnterpriseId { get; internal set; }

        /// <summary>
        ///     Managed Company Enterprise Name
        /// </summary>
        public string EnterpriseName { get; internal set; }

        /// <summary>
        ///     Managed Company Product ID
        /// </summary>
        public string ProductId { get; internal set; }

        /// <summary>
        ///     Number of Seats
        /// </summary>
        public int NumberOfSeats { get; internal set; }

        /// <summary>
        ///     Number of Users
        /// </summary>
        public int NumberOfUsers { get; internal set; }

        /// <summary>
        ///     File / Storage Plan Type
        /// </summary>
        public string FilePlanType { get; internal set; }

        /// <summary>
        ///     Is Managed Company Expired
        /// </summary>
        public bool IsExpired { get; internal set; }

        /// <summary>
        ///     Node that owns the managed company.
        /// </summary>
        public long ParentNodeId { get; internal set; }

        public ManagedCompanyLicenseAddOn[] AddOns { get; internal set; }

        /// <exclude />
        public long TreeKeyRole { get; internal set; }

        /// <exclude />
        public byte[] TreeKey { get; internal set; }
    }
}
