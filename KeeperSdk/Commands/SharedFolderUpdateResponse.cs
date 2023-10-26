using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class SharedFolderUpdateResponse : KeeperApiResponse
    {
        [DataMember(Name = "add_users")]
        public SharedFolderUpdateUserStatus[] addUsers;

        [DataMember(Name = "update_users")]
        public SharedFolderUpdateUserStatus[] updateUsers;

        [DataMember(Name = "remove_users")]
        public SharedFolderUpdateUserStatus[] removeUsers;

        [DataMember(Name = "add_teams")]
        public SharedFolderUpdateTeamStatus[] addTeams;

        [DataMember(Name = "update_teams")]
        public SharedFolderUpdateTeamStatus[] updateTeams;

        [DataMember(Name = "remove_teams")]
        public SharedFolderUpdateTeamStatus[] removeTeams;

        [DataMember(Name = "add_records")]
        public SharedFolderUpdateRecordStatus[] addRecords;

        [DataMember(Name = "update_records")]
        public SharedFolderUpdateRecordStatus[] updateRecords;

        [DataMember(Name = "remove_records")]
        public SharedFolderUpdateRecordStatus[] removeRecords;
    }
}
