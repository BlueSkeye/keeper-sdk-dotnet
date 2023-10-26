using KeeperSecurity.Commands;
using KeeperSecurity.Utils;
using System.Linq;

namespace KeeperSecurity.Authentication
{
    /// <summary>
    /// Represents user's account settings.
    /// </summary>
    public class AccountSettings : PasswordRequirements
    {
        /// <summary>
        /// 2FA is required.
        /// </summary>
        public bool? TwoFactorRequired { get; internal set; }

        /// <summary>
        /// 2FA channel.
        /// <list type="bullet">
        /// <item>
        /// <term>two_factor_disabled</term>
        /// <description>two factor is not enabled for this user</description>
        /// </item>
        /// <item>
        /// <term>two_factor_channel_sms</term>
        /// <description>TOTP codes are sent through SMS</description>
        /// </item>
        /// <item>
        /// <term>two_factor_channel_voice</term>
        /// <description>TOTP codes are sent through voice calls</description>
        /// </item>
        /// <item>
        /// <term>two_factor_channel_google</term>
        /// <description>Google/Microsoft Authenticator</description>
        /// </item>
        /// </list>
        /// </summary>
        public string Channel { get; internal set; }
        /// <summary>
        /// Parameter value for <see cref="Channel"/>
        /// </summary>
        public string ChannelValue { get; internal set; }
        /// <summary>
        /// Is email verified?
        /// </summary>
        public bool? EmailVerified { get; internal set; }
        /// <summary>
        /// Deadline to accept Account Transfer Consent. Unix timestamp.
        /// </summary>
        public double? MustPerformAccountShareBy { get; internal set; }
        /// <summary>
        /// Time of last change of master password. Unix timestamp.
        /// </summary>
        public double? MasterPasswordLastModified { get; internal set; }
        /// <summary>
        /// Theme.
        /// </summary>
        public string Theme { get; internal set; }
        /// <summary>
        /// Is SSO user?
        /// </summary>
        public bool? SsoUser { get; internal set; }
        /// <summary>
        /// Logout timeout in seconds.
        /// </summary>
        public long? LogoutTimerInSec { get; internal set; }
        /// <summary>
        /// Enterprise administrator requested data key sharing.
        /// </summary>
        public bool? ShareDatakeyWithEnterprise { get; internal set; }
        /// <summary>
        /// If true data key is nor shared with device key
        /// </summary>
        public bool? ShareDataKeyWithDevicePublicKey { get; internal set; }
        /// <summary> 
        /// Persistent login.
        /// </summary>
        public bool PersistentLogin { get; internal set; }
        /// <summary>
        /// Record types enabled flag.
        /// </summary>

        public bool RecordTypesEnabled { get; internal set; }

        internal string AccountFolderKey { get; set; }
        internal AccountShareTo[] ShareAccountTo { get; set; }

        internal static AccountSettings LoadFromProtobuf(AccountSummary.Settings settings)
        {
            return new AccountSettings
            {
                TwoFactorRequired = settings.TwoFactorRequired,
                Channel = settings.Channel,
                ChannelValue = settings.ChannelValue,
                EmailVerified = settings.EmailVerified,
                AccountFolderKey = settings.AccountFolderKey.ToByteArray().Base64UrlEncode(),
                MustPerformAccountShareBy = settings.MustPerformAccountShareBy > 0 ? (double?) settings.MustPerformAccountShareBy : null,
                ShareAccountTo = settings.ShareAccountTo.Select(x => new AccountShareTo
                {
                    PublicKey = x.PublicKey.ToByteArray().Base64UrlEncode(),
                    RoleId = x.RoleId
                }).ToArray(),
                MasterPasswordLastModified = settings.MasterPasswordLastModified > 1 ? (double?) settings.MasterPasswordLastModified : null,
                Theme = settings.Theme,
                SsoUser = settings.SsoUser,
                ShareDatakeyWithEnterprise = settings.ShareDataKeyWithEccPublicKey,
                ShareDataKeyWithDevicePublicKey = settings.ShareDataKeyWithDevicePublicKey,
                LogoutTimerInSec = settings.LogoutTimer > 1000 ? settings.LogoutTimer / 1000 : (long?) null,
                PersistentLogin = settings.PersistentLogin,
                RecordTypesEnabled = settings.RecordTypesEnabled,
            };
        }
    }
}
