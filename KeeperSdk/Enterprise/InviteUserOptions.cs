
namespace KeeperSecurity.Enterprise
{
    /// <summary>
    /// Defines optional Invite User properties 
    /// </summary>
    public class InviteUserOptions
    {
        /// <summary>
        /// User Full Name
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Enterprise Node ID
        /// </summary>
        public long? NodeId { get; set; }
    }
}
