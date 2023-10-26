using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class GetShareAutoCompleteCommand : AuthenticatedCommand
    {
        public GetShareAutoCompleteCommand() : base("get_share_auto_complete")
        {
        }

        [DataMember(Name = "starts_with", EmitDefaultValue = false)]
        public string StartsWith { get; set; }
    }
}
