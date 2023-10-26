using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    [SqlTable(Name = "SharedFolderKeys", PrimaryKey = new[] { "SharedFolderUid", "TeamUid" }, Index1 = new[] { "TeamUid" })]
    [DataContract]
    public class ExternalSharedFolderKey : IEntityLink, ISharedFolderKey, IEntityCopy<ISharedFolderKey>
    {
        [SqlColumn(Length = 32)]
        [DataMember(Name = "shared_folder_uid", EmitDefaultValue = false)]
        public string SharedFolderUid { get; set; }

        [SqlColumn(Length = 32)]
        [DataMember(Name = "team_uid", EmitDefaultValue = false)]
        public string TeamUid { get; set; }

        [SqlColumn]
        [DataMember(Name = "key_type")]
        public int KeyType { get; set; }

        [SqlColumn]
        [DataMember(Name = "shared_folder_key", EmitDefaultValue = false)]
        public string SharedFolderKey { get; set; }

        public string SubjectUid
        {
            get => SharedFolderUid;
            set => SharedFolderUid = value;
        }

        public string ObjectUid
        {
            get => TeamUid;
            set => TeamUid = value;
        }

        public void CopyFields(ISharedFolderKey source)
        {
            SharedFolderUid = source.SharedFolderUid;
            TeamUid = source.TeamUid;
            KeyType = source.KeyType;
            SharedFolderKey = source.SharedFolderKey;
        }
    }
}
