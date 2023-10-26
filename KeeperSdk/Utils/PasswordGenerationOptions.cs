
namespace KeeperSecurity.Utils
{
    /// <summary>
    /// Defines password generation rules.
    /// </summary>
    public class PasswordGenerationOptions
    {
        /// <summary>
        /// Password Length
        /// </summary>
        /// <remarks>Default: 20</remarks>
        public int Length { get; set; }
        /// <summary>
        /// Minimal number of lowercase characters. 
        /// </summary>
        /// <remarks>-1 to exclude lowercase characters</remarks>
        public int Lower { get; set; }
        /// <summary>
        /// Minimal number of uppercase characters. 
        /// </summary>
        /// <remarks>-1 to exclude uppercase characters</remarks>
        public int Upper { get; set; }
        /// <summary>
        /// Minimal number of digits
        /// </summary>
        /// <remarks>-1 to exclude digits</remarks>
        public int Digit { get; set; }
        /// <summary>
        /// Minimal number of special characters
        /// </summary>
        /// <remarks>-1 to exclude special characters</remarks>
        public int Special { get; set; }
        /// <summary>
        /// Special character vocabulary. <see cref="CryptoUtils.SPECIAL_CHARACTERS"/>
        /// </summary>
        public string SpecialCharacters { get; set; }
    }
}
