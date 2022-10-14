using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Resource.Follow;
using Microsoft.EntityFrameworkCore;

namespace ChattingApp.Persistence.Repositories
{
    public class FollowsRepository : IFollowsRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public FollowsRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        public IMapper Mapper { get; }

        public async Task<UserFollow> GetUserFollow(string sourceUserId, string sserFollowedId)
        {
          return  await appDbContext.Follows.FindAsync(sourceUserId, sserFollowedId);
        }

        public async Task<IEnumerable<FollowDto>> GetUserFollowers(string predicate, string userId)
        {
           var users = appDbContext.Users.OrderBy(u => u.UserName).AsQueryable();
            var followees = appDbContext.Follows.AsQueryable();
            if (predicate == "follow")
            {
                followees = followees.Where(l => l.SourceUserId == userId);
                users = followees.Select(u => u.UserFollowed);
            }
            if (predicate == "followby")
            {
                followees = followees.Where(l => l.UserFollowedId == userId);
                users = followees.Select(u => u.SourceUser);
            }
            return  mapper.Map<IEnumerable<FollowDto>>(users);
        }

        public async Task<AppUsers> GetUserWithFollowes(string userId)
        {
          return  await appDbContext.Users.Include(x => x.Followees).FirstOrDefaultAsync(x => x.Id==userId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await appDbContext.SaveChangesAsync() > 0;
        }
    }
}
