using Enterprise;
using System;
using System.Collections.Generic;
using KeeperSecurity.Utils;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using KeeperSecurity.Authentication;

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    /// Represents Role enterprise data.
    /// </summary>
    public class RoleData : EnterpriseDataPlugin, IRoleData
    {
        private readonly RoleDictionary _roles = new RoleDictionary();
        private readonly RoleUserLink _roleUsers = new RoleUserLink();
        private readonly RoleTeamLink _roleTeams = new RoleTeamLink();
        private readonly RoleEnforcementLink _roleEnforcements = new RoleEnforcementLink();
        private readonly ManagedNodeLink _managedNodes = new ManagedNodeLink();
        private readonly RolePrivilegesList _rolePrivileges = new RolePrivilegesList();

        public RoleData()
        {
            Entities = new IKeeperEnterpriseEntity[] { _roles, _roleUsers, _roleTeams, _roleEnforcements, _managedNodes, _rolePrivileges };
        }

        /// <exclude/>
        public override IEnumerable<IKeeperEnterpriseEntity> Entities { get; }

        /// <inheritdoc/>
        public IEnumerable<EnterpriseRole> Roles => _roles.Entities;

        /// <inheritdoc/>
        public int RoleCount => _roles.Count;


        /// <inheritdoc/>
        public bool TryGetRole(long roleId, out EnterpriseRole role)
        {
            return _roles.TryGetEntity(roleId, out role);
        }

        /// <inheritdoc/>
        public IEnumerable<long> GetUsersForRole(long roleId)
        {
            return _roleUsers.LinksForPrimaryKey(roleId).Select(x => x.EnterpriseUserId);
        }

        /// <inheritdoc/>
        public IEnumerable<long> GetRolesForUser(long userId)
        {
            return _roleUsers.LinksForSecondaryKey(userId).Select(x => x.RoleId);
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetTeamsForRole(long roleId)
        {
            return _roleTeams.LinksForPrimaryKey(roleId).Select(x => x.TeamUid.ToByteArray().Base64UrlEncode());
        }

        /// <inheritdoc/>
        public IEnumerable<long> GetRolesForTeam(string teamUid)
        {
            return _roleTeams.LinksForSecondaryKey(teamUid).Select(x => x.RoleId);
        }

        /// <inheritdoc/>
        public IEnumerable<RoleEnforcement> GetEnforcementsForRole(long roleId)
        {
            return _roleEnforcements.LinksForPrimaryKey(roleId);
        }

        /// <summary>
        /// Gets a list of privileges for specified role and node
        /// </summary>
        /// <param name="roleId">Enterprise Role ID.</param>
        /// <param name="nodeId">Enterprise Node ID.</param>
        /// <returns>List of Role Privileges</returns>
        public IEnumerable<RolePrivilege> GetPrivilegesForRoleAndNode(long roleId, long nodeId)
        {
            return _rolePrivileges.Entities.Where(x => x.RoleId == roleId && x.ManagedNodeId == nodeId);
        }

        /// <summary>
        /// Gets a list of all managed nodes in the enterprise
        /// </summary>
        /// <returns></returns>
        public IList<ManagedNode> GetManagedNodes() {
            return _managedNodes.GetAllLinks();
        }

        private Dictionary<long, byte[]> _adminRoleKeys = new Dictionary<long, byte[]>();

        /// <inheritdoc/>
        public async Task<byte[]> GetRoleKey(long roleId)
        {
            lock (_adminRoleKeys)
            {
                if (_adminRoleKeys.TryGetValue(roleId, out var result))
                {
                    return result;
                }
            }

            var krq = new GetEnterpriseDataKeysRequest();
            krq.RoleId.Add(roleId);
            var krs = await Enterprise.Auth.ExecuteAuthRest<GetEnterpriseDataKeysRequest, GetEnterpriseDataKeysResponse>("enterprise/get_enterprise_data_keys", krq);
            foreach (var rKey in krs.ReEncryptedRoleKey)
            {
                if (rKey.RoleId == roleId)
                {
                    try
                    {
                        var roleKey = CryptoUtils.DecryptAesV2(rKey.EncryptedRoleKey.ToByteArray(), Enterprise.TreeKey);
                        lock (_adminRoleKeys)
                        {
                            if (!_adminRoleKeys.ContainsKey(roleId))
                            {
                                _adminRoleKeys.Add(roleId, roleKey);
                            }
                            return roleKey;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }

            foreach (var rKey in krs.RoleKey)
            {
                if (rKey.RoleId == roleId)
                {
                    byte[] roleKey = null;
                    try
                    {
                        switch (rKey.KeyType)
                        {
                            case EncryptedKeyType.KtEncryptedByDataKey:
                                roleKey = CryptoUtils.DecryptAesV1(rKey.EncryptedKey.Base64UrlDecode(), Enterprise.Auth.AuthContext.DataKey);
                                break;
                            case EncryptedKeyType.KtEncryptedByDataKeyGcm:
                                roleKey = CryptoUtils.DecryptAesV2(rKey.EncryptedKey.Base64UrlDecode(), Enterprise.Auth.AuthContext.DataKey);
                                break;
                            case EncryptedKeyType.KtEncryptedByPublicKey:
                                roleKey = CryptoUtils.DecryptRsa(rKey.EncryptedKey.Base64UrlDecode(), Enterprise.Auth.AuthContext.PrivateRsaKey);
                                break;
                            case EncryptedKeyType.KtEncryptedByPublicKeyEcc:
                                if (Enterprise.Auth.AuthContext.PrivateEcKey != null)
                                {
                                    roleKey = CryptoUtils.DecryptEc(rKey.EncryptedKey.Base64UrlDecode(), Enterprise.Auth.AuthContext.PrivateEcKey);
                                }
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }

                    if (roleKey != null)
                    {
                        lock (_adminRoleKeys)
                        {
                            if (!_adminRoleKeys.ContainsKey(roleId))
                            {
                                _adminRoleKeys.Add(roleId, roleKey);
                            }
                            return roleKey;
                        }
                    }
                }
            }
            return null;
        }
    }
}
