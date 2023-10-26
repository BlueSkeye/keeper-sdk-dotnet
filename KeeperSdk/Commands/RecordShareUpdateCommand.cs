using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    internal class RecordShareUpdateCommand : AuthenticatedCommand
    {
        public RecordShareUpdateCommand() : base("record_share_update")
        {
            Pt = "Commander";
        }
        [DataMember(Name = "pt")]
        public string Pt;

        [DataMember(Name = "add_shares", EmitDefaultValue = false)]
        public RecordShareObject[] AddShares;

        [DataMember(Name = "update_shares", EmitDefaultValue = false)]
        public RecordShareObject[] UpdateShares;

        [DataMember(Name = "remove_shares", EmitDefaultValue = false)]
        public RecordShareObject[] RemoveShares;
    }
}
