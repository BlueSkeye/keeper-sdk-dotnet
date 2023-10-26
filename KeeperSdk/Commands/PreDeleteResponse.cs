using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class PreDeleteResponse : KeeperApiResponse
    {
        [DataMember(Name = "pre_delete_response", EmitDefaultValue = false)]
        public PreDeleteResponseObject preDeleteResponse;
    }
}
