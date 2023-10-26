using KeeperSecurity.Vault;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
#pragma warning disable 0649
    [DataContract]
    public class SyncDownRecord : IStorageRecord
    {
        [DataMember(Name = "record_uid")]
        public string RecordUid { get; set; }

        [DataMember(Name = "revision")]
        public long Revision { get; set; }

        [DataMember(Name = "version")]
        public int Version { get; set; }

        [DataMember(Name = "shared")]
        public bool Shared { get; set; }

        [DataMember(Name = "client_modified_time")]
        public long ClientModifiedTime { get; set; }

        [DataMember(Name = "data")]
        public string Data { get; set; }

        [DataMember(Name = "extra")]
        public string Extra { get; set; }

        [DataMember(Name = "udata")]
        public SyncDownRecordUData udata;

        [DataMember(Name = "owner_uid")]
        public string OwnerRecordId;

        [DataMember(Name = "link_key")]
        public string LinkKey;

        [DataMember(Name = "file_size")]
        internal long? fileSize;

        [DataMember(Name = "thumbnail_size")]
        internal long? thumbnailSize;

        public string Udata { get; set; }

        public bool Owner { get; set; }

        string IUid.Uid => RecordUid;
    }
}
