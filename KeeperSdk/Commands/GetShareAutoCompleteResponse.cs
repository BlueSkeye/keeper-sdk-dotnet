using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class GetShareAutoCompleteResponse : KeeperApiResponse
    {
        [DataMember(Name = "shares_from_users")]
        public ShareUserInfo[] SharesFromUsers;

        [DataMember(Name = "shares_with_users")]
        public ShareUserInfo[] SharesWithUsers;

        [DataMember(Name = "group_users	")]
        public ShareUserInfo[] GroupUsers;
    }
}
