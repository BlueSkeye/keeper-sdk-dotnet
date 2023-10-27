using Cli;
using Commander.Enterprise;
using KeeperSecurity.Commands;
using KeeperSecurity.Enterprise;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Commander
{
    internal class McEnterpriseContext : StateCommands, IEnterpriseContext
    {
        public EnterpriseLoader Enterprise { get; }
        public EnterpriseData EnterpriseData { get; }
        public DeviceApprovalData DeviceApproval { get; }
        public RoleDataManagement RoleManagement { get; }
        public QueuedTeamDataManagement QueuedTeamManagement { get; }
        public UserAliasData UserAliasData { get; }

        public McEnterpriseContext(ManagedCompanyAuth auth)
        {
            if (auth.AuthContext.IsEnterpriseAdmin)
            {
                DeviceApproval = new DeviceApprovalData();
                RoleManagement = new RoleDataManagement();
                EnterpriseData = new EnterpriseData();
                QueuedTeamManagement = new QueuedTeamDataManagement();
                UserAliasData = new UserAliasData();

                Enterprise = new EnterpriseLoader(auth, new EnterpriseDataPlugin[] { EnterpriseData, RoleManagement, DeviceApproval, QueuedTeamManagement, UserAliasData });
                Task.Run(async () =>
                {
                    try
                    {
                        await Enterprise.LoadKeys(auth.TreeKey);
                        await Enterprise.Load();
                        this.AppendEnterpriseCommands(this);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                });
            }
        }

        public bool AutoApproveAdminRequests { get; set; }
        public ECPrivateKeyParameters EnterprisePrivateKey { get; set; }
        public Dictionary<long, byte[]> UserDataKeys { get; } = new Dictionary<long, byte[]>();
        public IDictionary<string, AuditEventType> AuditEvents { get; set; }

        public override string GetPrompt()
        {
            return "Managed Company";
        }
    }
}
