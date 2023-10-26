using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KeeperSecurity.Commands
{
    /// <exclude/>
    [DataContract]
    public class SharedFolderUpdateUser
    {
        [DataMember(Name = "username", EmitDefaultValue = false)]
        public string Username { get; set; }

        [DataMember(Name = "manage_users", EmitDefaultValue = false)]
        public bool? ManageUsers { get; set; }

        [DataMember(Name = "manage_records", EmitDefaultValue = false)]
        public bool? ManageRecords { get; set; }

        [DataMember(Name = "shared_folder_key", EmitDefaultValue = false)]
        public string SharedFolderKey { get; set; }
    }
}
