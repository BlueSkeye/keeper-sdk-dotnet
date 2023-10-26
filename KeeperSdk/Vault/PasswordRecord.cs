using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Represents a Legacy Keeper Record.
    /// </summary>
    public class PasswordRecord : KeeperRecord
    {
        /// <summary>
        /// Notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Login or Username.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Web URL.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// TOTP URL.
        /// </summary>
        public string Totp { get; set; }

        /// <summary>
        /// A list of Custom Fields.
        /// </summary>
        public IList<CustomField> Custom { get; } = new List<CustomField>();

        /// <summary>
        /// A list of Attachments.
        /// </summary>
        public IList<AttachmentFile> Attachments { get; } = new List<AttachmentFile>();
    }
}
