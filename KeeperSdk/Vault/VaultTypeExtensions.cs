using System.Collections.Generic;

namespace KeeperSecurity.Vault
{
    internal static class VaultTypeExtensions
    {
        private static readonly IDictionary<FolderType, string> FolderTypes = new Dictionary<FolderType, string>
        {
            {FolderType.UserFolder, "user_folder"},
            {FolderType.SharedFolder, "shared_folder"},
            {FolderType.SharedFolderFolder, "shared_folder_folder"},
        };

        public static string GetFolderTypeText(this FolderType folderType)
        {
            return FolderTypes[folderType];
        }
    }
}
