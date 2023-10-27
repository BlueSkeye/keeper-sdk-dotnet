using Cli;
using Commander.Enterprise;
using KeeperSecurity.Authentication;
using KeeperSecurity.Commands;
using KeeperSecurity.Enterprise;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Commander
{
    public partial class ConnectedContext : IEnterpriseContext
    {
        public EnterpriseLoader Enterprise { get; private set; }
        public EnterpriseData EnterpriseData { get; private set; }
        public RoleDataManagement RoleManagement { get; private set; }
        public QueuedTeamDataManagement QueuedTeamManagement { get; private set; }
        public UserAliasData UserAliasData { get; internal set; }

        public DeviceApprovalData DeviceApproval { get; private set; }
        public bool AutoApproveAdminRequests { get; set; }
        public Dictionary<long, byte[]> UserDataKeys { get; } = new Dictionary<long, byte[]>();


        public ECPrivateKeyParameters EnterprisePrivateKey { get; set; }
        public IDictionary<string, AuditEventType> AuditEvents { get; set; }

        private ManagedCompanyData _managedCompanies;

        private void CheckIfEnterpriseAdmin()
        {
            if (_auth.AuthContext.IsEnterpriseAdmin)
            {
                EnterpriseData = new EnterpriseData();
                RoleManagement = new RoleDataManagement();
                DeviceApproval = new DeviceApprovalData();
                _managedCompanies = new ManagedCompanyData();
                QueuedTeamManagement = new QueuedTeamDataManagement();
                UserAliasData = new UserAliasData();

                Enterprise = new EnterpriseLoader(_auth, new EnterpriseDataPlugin[] { EnterpriseData, RoleManagement, DeviceApproval, _managedCompanies, QueuedTeamManagement, UserAliasData });

                _auth.PushNotifications?.RegisterCallback(EnterpriseNotificationCallback);
                Task.Run(async () =>
                {
                    try
                    {
                        await Enterprise.Load();

                        this.AppendEnterpriseCommands(this);

                        if (!string.IsNullOrEmpty(EnterpriseData.EnterpriseLicense?.LicenseStatus) && EnterpriseData.EnterpriseLicense.LicenseStatus.StartsWith("msp"))
                        {
                            Commands.Add("mc-list",
                                new Cli.SimpleCommand
                                {
                                    Order = 72,
                                    Description = "List managed companies",
                                    Action = ListManagedCompanies,
                                });
                            Commands.Add("mc-create",
                                new ParseableCommand<ManagedCompanyCreateOptions>
                                {
                                    Order = 73,
                                    Description = "Create managed company",
                                    Action = CreateManagedCompany,
                                });
                            Commands.Add("mc-update",
                                new ParseableCommand<ManagedCompanyUpdateOptions>
                                {
                                    Order = 74,
                                    Description = "Updates managed company",
                                    Action = UpdateManagedCompany,
                                });
                            Commands.Add("mc-delete",
                                new ParseableCommand<ManagedCompanyRemoveOptions>
                                {
                                    Order = 75,
                                    Description = "Removes managed company",
                                    Action = RemoveManagedCompany,
                                });
                            Commands.Add("mc-login",
                                new ParseableCommand<ManagedCompanyLoginOptions>
                                {
                                    Order = 79,
                                    Description = "Login to managed company",
                                    Action = LoginToManagedCompany,
                                });
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                });
            }
        }

        private bool EnterpriseNotificationCallback(NotificationEvent evt)
        {
            if (evt.Event == "request_device_admin_approval")
            {
                if (AutoApproveAdminRequests)
                {
                    Task.Run(async () =>
                    {
                        await Enterprise.Load();
                        if (!EnterpriseData.TryGetUserByEmail(evt.Email, out var user)) return;

                        var devices = DeviceApproval.DeviceApprovalRequests
                            .Where(x => x.EnterpriseUserId == user.Id)
                            .ToArray();
                        await this.ApproveAdminDeviceRequests(devices);
                        Console.WriteLine($"Auto approved {evt.Email} at IP Address {evt.IPAddress}.");
                    });
                }
                else
                {
                    Console.WriteLine($"\n{evt.Email} requested Device Approval\nIP Address: {evt.IPAddress}\nDevice Name: {evt.DeviceName}");
                }
            }

            return false;
        }

        private async Task LoginToManagedCompany(ManagedCompanyLoginOptions options)
        {
            var mcAuth = new ManagedCompanyAuth();
            await mcAuth.LoginToManagedCompany(Enterprise, options.CompanyId);
            NextStateCommands = new McEnterpriseContext(mcAuth);
        }

        private Task ListManagedCompanies(string _)
        {
            var tab = new Tabulate(9);
            tab.AddHeader("Company Name", "Company ID", "Node", "Plan", "Storage", "Addons", "Seats Allowed", "Seats Used", "Paused");
            foreach (var mc in _managedCompanies.ManagedCompanies)
            {
                string nodeName = "";
                if (EnterpriseData.TryGetNode(mc.ParentNodeId, out var node))
                {
                    if (node.ParentNodeId > 0)
                    {
                        nodeName = node.DisplayName;
                    }
                    else
                    {
                        nodeName = EnterpriseData.Enterprise.EnterpriseName;
                    }
                }
                var plan = ManagedCompanyConstants.MspProducts.FirstOrDefault(x => x.ProductCode == mc.ProductId);
                var filePlan = ManagedCompanyConstants.MspFilePlans.FirstOrDefault(x => x.FilePlanCode == mc.FilePlanType);
                var addons = mc.AddOns.Select(x =>
                {
                    var addon = ManagedCompanyConstants.MspAddons.FirstOrDefault(y => x.Name == y.AddonCode);
                    return addon?.AddonName ?? x.Name;
                }).ToArray();
                tab.AddRow(mc.EnterpriseName, mc.EnterpriseId, nodeName, plan?.ProductName ?? mc.ProductId, filePlan?.FilePlanName ?? mc.FilePlanType,
                    addons, mc.NumberOfSeats < 2000000 ? mc.NumberOfSeats : (object) "Unlimited", mc.NumberOfUsers, mc.IsExpired ? "Yes" : "");
            }
            tab.Sort(0);
            tab.DumpRowNo = true;
            tab.SetColumnRightAlign(6, true);
            tab.SetColumnRightAlign(7, true);
            tab.Dump();
            return Task.CompletedTask;
        }


        private void PopulateMspCommonOptions(ManagedCompanyCommonOptions arguments, ManagedCompanyOptions options)
        {
            if (!string.IsNullOrEmpty(arguments.Node))
            {
                var n = EnterpriseData.ResolveNodeName(arguments.Node);
                options.NodeId = n.Id;
            }

            if (!string.IsNullOrEmpty(arguments.Product))
            {
                var plan = ManagedCompanyConstants.MspProducts.FirstOrDefault(x => string.Equals(arguments.Product, x.ProductCode, StringComparison.InvariantCultureIgnoreCase));
                if (plan == null)
                {
                    throw new Exception($"Invalid license plan: {arguments.Product}. Supported plans are {string.Join(", ", ManagedCompanyConstants.MspProducts.Select(x => x.ProductCode))}");
                }
                options.ProductId = plan.ProductCode;
            }

            if (arguments.Seats != null)
            {
                options.NumberOfSeats = arguments.Seats.Value >= 0 ? arguments.Seats.Value : 2147483647;
            }

            if (!string.IsNullOrEmpty(arguments.Storage))
            {
                var filePlan = ManagedCompanyConstants.MspFilePlans.FirstOrDefault(x =>
                string.Equals(arguments.Storage, x.FilePlanName, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(arguments.Storage, x.FilePlanCode, StringComparison.InvariantCultureIgnoreCase));

                if (filePlan == null)
                {
                    throw new Exception($"Invalid storage plan: {arguments.Storage}. Supported plans are {string.Join(", ", ManagedCompanyConstants.MspProducts.Select(x => x.ProductName))}");
                }
                options.FilePlanType = filePlan.FilePlanCode;
            }

            if (!string.IsNullOrEmpty(arguments.Addons))
            {
                var addonList = new List<ManagedCompanyAddonOptions>();
                foreach (var aon in arguments.Addons.Split(','))
                {
                    string addonName = aon.Trim();
                    if (string.IsNullOrEmpty(addonName))
                    {
                        continue;
                    }
                    int addonSeats = 0;
                    var pos = addonName.IndexOf(':');
                    if (pos > 0)
                    {
                        var seats = addonName.Substring(pos + 1);
                        addonName = addonName.Substring(0, pos);
                        if (!int.TryParse(seats, out addonSeats))
                        {
                            throw new Exception($"Invalid number of seats \"{seats}\" for addon \"{addonName}\"");
                        }
                    }
                    var addon = ManagedCompanyConstants.MspAddons.FirstOrDefault(x => string.Equals(x.AddonCode, addonName, StringComparison.InvariantCultureIgnoreCase));
                    if (addon == null)
                    {
                        throw new Exception($"Invalid addon {addonName}. Supported addons are {string.Join(", ", ManagedCompanyConstants.MspAddons.Select(x => x.AddonCode))}");
                    }
                    addonList.Add(new ManagedCompanyAddonOptions
                    {
                        Addon = addon.AddonCode,
                        NumberOfSeats = addonSeats > 0 ? addonSeats : (int?) null
                    });
                }
                options.Addons = addonList.ToArray();
            }
        }

        private async Task CreateManagedCompany(ManagedCompanyCreateOptions arguments)
        {
            var mcOptions = new ManagedCompanyOptions
            {
                NodeId = EnterpriseData.RootNode.Id,
                Name = arguments.Name,
            };

            PopulateMspCommonOptions(arguments, mcOptions);

            if (string.IsNullOrEmpty(mcOptions.ProductId))
            {
                throw new Exception($"License plan is required.");
            }

            if (mcOptions.NumberOfSeats == null)
            {
                mcOptions.NumberOfSeats = 0;
            }

            var mc = await _managedCompanies.CreateManagedCompany(mcOptions);
            Console.WriteLine($"Managed Company \"{mc.EnterpriseName}\", ID:{mc.EnterpriseId} has been created.");
        }


        private async Task UpdateManagedCompany(ManagedCompanyUpdateOptions arguments)
        {
            int companyId = -1;
            int.TryParse(arguments.Company, out companyId);

            var mc = _managedCompanies.ManagedCompanies.FirstOrDefault(x =>
            {
                if (companyId > 0)
                {
                    if (companyId == x.EnterpriseId)
                    {
                        return true;
                    }
                }

                return string.Equals(x.EnterpriseName, arguments.Company, StringComparison.InvariantCultureIgnoreCase);
            });

            if (mc == null)
            {
                Console.WriteLine($"Managed company {arguments.Company} not found.");
            }

            var mcOptions = new ManagedCompanyOptions
            {
                ProductId = mc.ProductId,
                NumberOfSeats = mc.NumberOfSeats
            };
            PopulateMspCommonOptions(arguments, mcOptions);

            if (!string.IsNullOrEmpty(arguments.Name))
            {
                mcOptions.Name = arguments.Name;
            }

            var mc1 = await _managedCompanies.UpdateManagedCompany(mc.EnterpriseId, mcOptions);

            Console.WriteLine($"Managed Company \"{mc1.EnterpriseName}\", ID:{mc1.EnterpriseId} has been updated.");
        }

        private async Task RemoveManagedCompany(ManagedCompanyRemoveOptions options)
        {
            int companyId = -1;
            int.TryParse(options.Company, out companyId);

            var mc = _managedCompanies.ManagedCompanies.FirstOrDefault(x =>
            {
                if (companyId > 0)
                {
                    if (companyId == x.EnterpriseId)
                    {
                        return true;
                    }
                }

                return string.Equals(x.EnterpriseName, options.Company, StringComparison.InvariantCultureIgnoreCase);
            });

            if (mc != null)
            {
                await _managedCompanies.RemoveManagedCompany(mc.EnterpriseId);
                Console.WriteLine($"Managed Company \"{mc.EnterpriseName}\", ID:{mc.EnterpriseId} has been removed.");
            }
            else
            {
                Console.WriteLine($"Managed company {options.Company} not found.");
            }
        }
    }
}
