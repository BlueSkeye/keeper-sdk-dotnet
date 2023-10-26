using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    [SqlTable(Name = "Teams", PrimaryKey = new[] { "TeamUid" })]
    [DataContract]
    public class ExternalEnterpriseTeam : IEntity, IEnterpriseTeam, IEntityCopy<IEnterpriseTeam>
    {
        [SqlColumn(Length = 32)]
        [DataMember(Name = "team_uid", EmitDefaultValue = false)]
        public string TeamUid { get; set; }

        [SqlColumn(Length = 256)]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [SqlColumn]
        [DataMember(Name = "team_key", EmitDefaultValue = false)]
        public string TeamKey { get; set; }

        [SqlColumn]
        [DataMember(Name = "key_type")]
        public int KeyType { get; set; }

        [SqlColumn]
        [DataMember(Name = "team_private_key", EmitDefaultValue = false)]
        public string TeamPrivateKey { get; set; }

        [SqlColumn]
        [DataMember(Name = "restrict_edit")]
        public bool RestrictEdit { get; set; }

        [SqlColumn]
        [DataMember(Name = "restrict_share")]
        public bool RestrictShare { get; set; }

        [SqlColumn]
        [DataMember(Name = "restrict_view")]
        public bool RestrictView { get; set; }

        public string Uid
        {
            get => TeamUid;
            set => TeamUid = value;
        }

        public void CopyFields(IEnterpriseTeam source)
        {
            TeamUid = source.TeamUid;
            Name = source.Name;
            TeamKey = source.TeamKey;
            KeyType = source.KeyType;
            TeamPrivateKey = source.TeamPrivateKey;
            RestrictEdit = source.RestrictEdit;
            RestrictShare = source.RestrictShare;
            RestrictView = source.RestrictView;
        }
    }
}
