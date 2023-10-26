using KeeperSecurity.Configuration;
using KeeperSecurity.Vault;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperSecurity.Utils
{
    /// <exclude/>
    public interface IExternalLoader
    {
        bool VerifyDatabase();
        IConfigurationStorage GetConfigurationStorage(string configurationName, IConfigurationProtectionFactory protection);
        IKeeperStorage GetKeeperStorage(string username);
    }
}
