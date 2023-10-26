using BI;
using Enterprise;
using KeeperSecurity.Authentication;
using KeeperSecurity.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace KeeperSecurity.Enterprise
{
    /// <exclude />
    public class ManagedCompanyDictionary : EnterpriseDataDictionary<int, ManagedCompany, EnterpriseManagedCompany>, IGetEnterprise
    {
        public ManagedCompanyDictionary() : base(EnterpriseDataEntity.ManagedCompanies)
        {
        }

        protected override int GetEntityId(ManagedCompany keeperData)
        {
            return keeperData.McEnterpriseId;
        }

        protected override void SetEntityId(EnterpriseManagedCompany entity, int id)
        {
            entity.EnterpriseId = id;
        }

        protected override void PopulateSdkFromKeeper(EnterpriseManagedCompany sdk, ManagedCompany keeper)
        {
            sdk.EnterpriseName = keeper.McEnterpriseName;
            sdk.ProductId = keeper.ProductId;
            sdk.NumberOfSeats = keeper.NumberOfSeats;
            sdk.NumberOfUsers = keeper.NumberOfUsers;
            sdk.ParentNodeId = keeper.MspNodeId;
            sdk.IsExpired = keeper.IsExpired;
            sdk.FilePlanType = keeper.FilePlanType;
            sdk.TreeKeyRole = keeper.TreeKeyRole;
            var treeKeyEncoded = keeper.TreeKey;
            if (!string.IsNullOrEmpty(treeKeyEncoded))
            {
                try
                {
                    var enterprise = GetEnterprise?.Invoke();
                    if (enterprise?.TreeKey != null)
                    {
                        sdk.TreeKey = CryptoUtils.DecryptAesV2(treeKeyEncoded.Base64UrlDecode(), enterprise.TreeKey);
                    }
                }
                catch { }
            }
            sdk.AddOns = keeper.AddOns.Select(x => new ManagedCompanyLicenseAddOn
            {
                Name = x.Name,
                Seats = x.Seats,
                IsEnabled = x.Enabled,
                IsTrial = x.IsTrial,
                Expiration = x.Expiration,
                Creation = x.Created,
                Activation = x.ActivationTime,
            }).ToArray();
        }

        internal readonly Dictionary<string, MspPrice> _prices = new Dictionary<string, MspPrice>();

        protected override void DataStructureChanged()
        {
            if (_prices.Count > 0)
            {
                return;
            }
            lock (this)
            {
                if (_prices.Count > 0)
                {
                    return;
                }
                var enterprise = GetEnterprise?.Invoke();
                if (enterprise == null)
                {
                    return;
                }

                var names = new Dictionary<int, string>();
                names[1] = ManagedCompanyConstants.BusinessLicense;
                names[2] = ManagedCompanyConstants.BusinessPlusLicense;
                names[10] = ManagedCompanyConstants.EnterpriseLicense;
                names[11] = ManagedCompanyConstants.EnterprisePlusLicense;

                names[400] = ManagedCompanyConstants.StoragePlan100GB;
                names[700] = ManagedCompanyConstants.StoragePlan1TB;
                names[800] = ManagedCompanyConstants.StoragePlan10TB;

                try
                {
                    var endpoint = enterprise.Auth.GetBiUrl("mapping/addons");
                    var rq = new MappingAddonsRequest();
                    var rs = enterprise.Auth.ExecuteAuthRest<MappingAddonsRequest, MappingAddonsResponse>(endpoint, rq).GetAwaiter().GetResult();
                    foreach (var fp in rs.FilePlans)
                    {
                        names[fp.Id * 100] = fp.Name;
                    }
                    foreach (var ap in rs.Addons)
                    {
                        names[ap.Id * 10000] = ap.Name;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    var endpoint = enterprise.Auth.GetBiUrl("subscription/mc_pricing");
                    var rq = new SubscriptionMcPricingRequest();
                    var rs = enterprise.Auth.ExecuteAuthRest<SubscriptionMcPricingRequest, SubscriptionMcPricingResponse>(endpoint, rq).GetAwaiter().GetResult();
                    foreach (var bp in rs.BasePlans)
                    {
                        if (names.TryGetValue(bp.Id, out var name))
                        {
                            _prices[name] = new MspPrice
                            {
                                Amount = (float) bp.Cost.Amount,
                                Currency = bp.Cost.Currency,
                                Rate = bp.Cost.AmountPer,
                            };
                        }
                    }
                    foreach (var fp in rs.FilePlans)
                    {
                        if (names.TryGetValue(fp.Id * 100, out var name))
                        {
                            _prices[name] = new MspPrice
                            {
                                Amount = (float) fp.Cost.Amount,
                                Currency = fp.Cost.Currency,
                                Rate = fp.Cost.AmountPer,
                            };
                        }
                    }
                    foreach (var ap in rs.Addons)
                    {
                        if (names.TryGetValue(ap.Id * 10000, out var name))
                        {
                            _prices[name] = new MspPrice
                            {
                                Amount = (float) ap.Cost.Amount,
                                Currency = ap.Cost.Currency,
                                Rate = ap.Cost.AmountPer,
                                AmountConsumed = ap.AmountConsumed,
                            };
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

            }
        }

        public Func<IEnterpriseLoader> GetEnterprise { get; set; }
    }
}
