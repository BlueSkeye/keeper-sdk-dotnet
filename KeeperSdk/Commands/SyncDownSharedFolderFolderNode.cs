using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownSharedFolderFolderNode
    {
        [DataMember(Name = "folder_uid")]
        public string FolderUid { get; set; }

        [DataMember(Name = "parent_uid")]
        public string ParentUid { get; set; }

        [DataMember(Name = "shared_folder_uid")]
        public string SharedFolderUid { get; set; }
    }
}
