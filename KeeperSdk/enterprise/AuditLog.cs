using System;
using System.Globalization;
using System.Threading.Tasks;
using KeeperSecurity.Authentication;
using KeeperSecurity.Commands;
#if NET452_OR_GREATER
using KeeperSecurity.Utils;
#endif

namespace KeeperSecurity.Enterprise
{
    /// <summary>
    /// Enterprise Audit Log access methods.
    /// </summary>
    public static class AuditLogExtensions
    {
        /// <summary>
        /// Gets the list of all available audit events
        /// </summary>
        /// <param name="auth">Keeper Connection</param>
        /// <returns>Awaitable task returning supported audit events</returns>
        public static async Task<AuditEventType[]> GetAvailableEvents(this IAuthentication auth)
        {
            var rq = new GetAuditEventDimensionsCommand();
            var rs = await auth.ExecuteAuthCommand<GetAuditEventDimensionsCommand, GetAuditEventDimensionsResponse>(rq);
            return rs.Dimensions?.AuditEventTypes;
        }

        /// <summary>
        /// Gets audit events in descending order.
        /// </summary>
        /// <param name="auth">Keeper Connection</param>
        /// <param name="forUser">User email</param>
        /// <param name="recentUnixTime">Recent event epoch time in seconds</param>
        /// <param name="latestUnixTime">Latest event epoch time in seconds</param>
        /// <returns>Awaitable task returning a tuple. Item1 contains the audit event list. Item2 the epoch time in seconds to resume</returns>
        /// <remarks>This method returns first 1000 events. To get the next chunk of audit events pass the second parameter of result into <c>recentUnixTime</c> parameter.</remarks>
        public static async Task<Tuple<GetAuditEventReportsResponse, long>> GetUserEvents(this IAuthentication auth, string forUser, long recentUnixTime, long latestUnixTime = 0)
        {
            if (recentUnixTime < 0 || latestUnixTime < 0 || string.IsNullOrEmpty(forUser))
            {
                return null;
            }

            if (recentUnixTime == 0)
            {
                recentUnixTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000;
            }

            var rq = new GetAuditEventReportsCommand
            {
                Filter = new ReportFilter
                {
                    Username = forUser,
                    Created = new CreatedFilter
                    {
                        Max = recentUnixTime == 0 ? (long?) null : recentUnixTime,
                        Min = latestUnixTime == 0 ? (long?) null : latestUnixTime
                    }
                },
                Limit = 1000,
                ReportType = "raw",
                Order = "descending"

            };

            var rs = await auth.ExecuteAuthCommand<GetAuditEventReportsCommand, GetAuditEventReportsResponse>(rq);
            var response = Tuple.Create<GetAuditEventReportsResponse, long>(rs, -1);
            if (rs.Events == null || rs.Events.Count == 0) return response;

            if (rq.Limit > 0 && rs.Events?.Count < 0.95 * rq.Limit) return response;


            var pos = rs.Events.Count - 1;
            if (!rs.Events[pos].TryGetValue("created", out var lastCreated)) return response;

            while (pos > 0)
            {
                pos--;
                if (rs.Events[pos].TryGetValue("created", out var created))
                {
                    if (!Equals(created, lastCreated))
                    {
                        break;
                    }
                }
            }

            if (pos <= 0 || pos >= rs.Events.Count - 1) return response;
            if (!(lastCreated is IConvertible conv)) return response;

            rs.Events.RemoveRange(pos + 1, rs.Events.Count - pos - 1);
            return Tuple.Create(rs, conv.ToInt64(CultureInfo.InvariantCulture) + 1);
        }
    }
}
