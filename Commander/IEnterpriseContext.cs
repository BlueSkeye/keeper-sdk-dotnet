using Commander.Enterprise;
using KeeperSecurity.Commands;
using KeeperSecurity.Enterprise;
using Org.BouncyCastle.Crypto.Parameters;
using System.Collections.Generic;

namespace Commander
{
    internal interface IEnterpriseContext
    {
        EnterpriseLoader Enterprise { get; }
        EnterpriseData EnterpriseData { get; }
        RoleDataManagement RoleManagement { get; }
        QueuedTeamDataManagement QueuedTeamManagement { get; }
        UserAliasData UserAliasData { get; }

        DeviceApprovalData DeviceApproval { get; }

        bool AutoApproveAdminRequests { get; set; }
        Dictionary<long, byte[]> UserDataKeys { get; }

        ECPrivateKeyParameters EnterprisePrivateKey { get; set; }

        IDictionary<string, AuditEventType> AuditEvents { get; set; }
    }
}
