using System.Threading.Tasks;

namespace Commander.Enterprise
{
    public interface IQueuedTeamDataManagement
    {
        Task QueueUserToTeam(long enterpriseUserId, string teamUid);
    }
}
