using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Extensions;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Resource.Follow;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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

        public void AddNewFollowee(UserFollow userFollow)
        {
            appDbContext.Follows.Add(userFollow);
        }

        public async Task<UserFollow> GetUserFollow(string sourceUserId, string sserFollowedId)
        {
          return  await appDbContext.Follows.FindAsync(sourceUserId, sserFollowedId);
        }

        public async Task<IEnumerable<FollowDto>> GetUserFollowers(string predicate, string userId)
        {
            var users = appDbContext.Users.Include(p=>p.Photos).OrderBy(u => u.UserName).Include(p=>p.Photos).ToList();
            var followees = appDbContext.Follows.ToList();
            if (predicate == "follow")
            {
                followees = followees.Where(l => l.SourceUserId == userId).ToList();
                users = followees.Select(u => u.UserFollowed).ToList(); 
            }
            if (predicate == "followby")
            {
                //check list if empty fire exception in mapping
                followees = followees.Where(l => l.UserFollowedId == userId).ToList();
                users = followees.Select(u => u.SourceUser).ToList();
            }
            return mapper.Map<IEnumerable<FollowDto>>(users);

        }


        public async Task<AppUsers> GetUserWithFollowes(string userId)
        {
          return  await appDbContext.Users.Include(x => x.Followees).FirstOrDefaultAsync(x => x.Id==userId);
        }

    }
}
