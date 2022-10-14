using ChattingApp.Domain.Models;
using ChattingApp.Resource.Follow;

namespace ChattingApp.Persistence.IRepositories
{
    public interface IFollowsRepository
    {
        Task<UserFollow> GetUserFollow(string sourceUserId, string userFollowedId);
        Task<AppUsers> GetUserWithFollowes(string userId);
        Task<IEnumerable<FollowDto>> GetUserFollowers(string predicate,string userId);
    }
}
