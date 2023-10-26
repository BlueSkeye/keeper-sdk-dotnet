using KeeperSecurity.Authentication;
using KeeperSecurity.Commands;
using KeeperSecurity.Utils;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    /// Represends Managed Companies enterprise data.
    /// </summary>
    public partial class ManagedCompanyData : EnterpriseDataPlugin, IManagedCompanyData
    {
        private readonly ManagedCompanyDictionary _managedCompanies;

        public ManagedCompanyData()
        {
            _managedCompanies = new ManagedCompanyDictionary();
            Entities = new IKeeperEnterpriseEntity[] { _managedCompanies };
        }


        /// <exclude />
        public override IEnumerable<IKeeperEnterpriseEntity> Entities { get; }

        /// <summary>
        /// Get a list of all managed companies in the enterprise.
        /// </summary>
        public IEnumerable<EnterpriseManagedCompany> ManagedCompanies => _managedCompanies.Entities;

        /// <inheritdoc/>
        public async Task<EnterpriseManagedCompany> CreateManagedCompany(ManagedCompanyOptions options)
        {
            if (string.IsNullOrEmpty(options.Name))
            {
                options.Name = CryptoUtils.GenerateUid();
            }

            var treeKey = CryptoUtils.GenerateEncryptionKey();
            var encryptedTreeKey = CryptoUtils.EncryptAesV2(treeKey, Enterprise.TreeKey);

            var encData = new EncryptedData
            {
                DisplayName = "Keeper Administrator"
            };
            var encryptedRoleData = CryptoUtils.EncryptAesV1(JsonUtils.DumpJson(encData), treeKey);

            encData.DisplayName = "root";
            var encryptedNodeData = CryptoUtils.EncryptAesV1(JsonUtils.DumpJson(encData), treeKey);

            var rq = new EnterpriseRegistrationByMspCommand
            {
                NodeId = options.NodeId,
                Seats = options.NumberOfSeats ?? 0,
                ProductId = options.ProductId,
                FilePlanType = options.FilePlanType,
                EnterpriseName = options.Name,
                EncryptedTreeKey = encryptedTreeKey.Base64UrlEncode(),
                RoleData = encryptedRoleData.Base64UrlEncode(),
                RootNode = encryptedNodeData.Base64UrlEncode(),
            };
            if (options.Addons != null)
            {
                rq.AddOns = options.Addons.Select(x => new Commands.MspAddon
                {
                    AddOn = x.Addon,
                    Seats = x.NumberOfSeats
                }).ToArray();
            }

            var rs = await Enterprise.Auth.ExecuteAuthCommand<EnterpriseRegistrationByMspCommand, EnterpriseManagedCompanyByMspResponse>(rq);
            await Enterprise.Load();

            return ManagedCompanies.FirstOrDefault(x => x.EnterpriseId == rs.EnterpriseId);
        }

        /// <inheritdoc/>
        public async Task<EnterpriseManagedCompany> UpdateManagedCompany(int companyId, ManagedCompanyOptions options)
        {
            if (!_managedCompanies.TryGetEntity(companyId, out var mc))
            {
                throw new EnterpriseException($"Managed Company #{companyId} does not exist");
            }

            var rq = new EnterpriseUpdateByMspCommand
            {
                EnterpriseId = companyId,
                NodeId = options.NodeId,
                FilePlanType = options.FilePlanType,
                EnterpriseName = options.Name ?? mc.EnterpriseName,
                Seats = options.NumberOfSeats ?? mc.NumberOfSeats,
                ProductId = options.ProductId ?? mc.ProductId,
            };
            if (options.Addons != null)
            {
                rq.AddOns = options.Addons.Select(x => new Commands.MspAddon
                {
                    AddOn = x.Addon,
                    Seats = x.NumberOfSeats
                }).ToArray();
            }

            var rs = await Enterprise.Auth.ExecuteAuthCommand<EnterpriseUpdateByMspCommand, EnterpriseManagedCompanyByMspResponse>(rq);
            await Enterprise.Load();

            return ManagedCompanies.FirstOrDefault(x => x.EnterpriseId == companyId);
        }


        /// <inheritdoc/>
        public async Task RemoveManagedCompany(int companyId)
        {
            var rq = new EnterpriseRemoveByMspCommand
            {
                EnterpriseId = companyId,
            };

            await Enterprise.Auth.ExecuteAuthCommand(rq);
            await Enterprise.Load();
        }
    }
}
