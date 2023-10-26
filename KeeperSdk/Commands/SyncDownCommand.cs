using System;
using System.Runtime.Serialization;

namespace KeeperSecurity.Commands
{
    [DataContract]
    public class SyncDownCommand : AuthenticatedCommand
    {
        public SyncDownCommand() : base("sync_down")
        {
            clientTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        [DataMember(Name = "revision")]
        public long revision;

        [DataMember(Name = "include")]
        public string[] include;

        [DataMember(Name = "device_name")]
        public string deviceName;

        [DataMember(Name = "client_time")]
        public long clientTime;
    }
}
