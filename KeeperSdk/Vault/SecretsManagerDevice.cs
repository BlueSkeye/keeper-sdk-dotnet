using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperSecurity.Vault
{
    public class SecretsManagerDevice
    {
        public string Name { get; internal set; }
        public string DeviceId { get; internal set; }
        public DateTimeOffset CreatedOn { get; internal set; }
        public DateTimeOffset? FirstAccess { get; internal set; }
        public DateTimeOffset? LastAccess { get; internal set; }
        public byte[] PublicKey { get; internal set; }
        public bool LockIp { get; internal set; }
        public string IpAddress { get; internal set; }
        public DateTimeOffset? FirstAccessExpireOn { get; internal set; }
        public DateTimeOffset? AccessExpireOn { get; internal set; }
    }
}
