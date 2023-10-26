using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    /// <summary>
    /// Defines access methods for compound record types
    /// </summary>
    public interface IFieldTypeSerialize
    {
        /// <summary>
        /// Enumerates property names
        /// </summary>
        IEnumerable<string> Elements { get; }
        /// <summary>
        /// Enumerates property values
        /// </summary>
        IEnumerable<string> ElementValues { get; }
        /// <summary>
        /// Sets property value
        /// </summary>
        /// <param name="element">Property or element name</param>
        /// <param name="value">Property value</param>
        /// <returns>true is the property was set</returns>
        bool SetElementValue(string element, string value);
    }
}
