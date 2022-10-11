using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChattingApp.Domain.Models;
using ChattingApp.Helper.Pagination;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Resource.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChattingApp.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<AppUsers> userManager;
        private readonly IMapper mapper;

        public UserRepository(AppDbContext appDbContext ,UserManager<AppUsers> userManager,IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
            this.mapper = mapper;
        }

 
        public async Task<UserResponseDto> GetUserByIdAsync(string Id)
        {
          var  user=  await appDbContext.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Id == Id);
            return mapper.Map<UserResponseDto>(user);
           
        }

        public async Task<AppUsers> GetUserByNameAsync(string UserName)
        {
            return await appDbContext.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.UserName == UserName);
        }

        public async Task<PagedList<AppUsers>> GetUsersAsync(UserReqDto userReqDto)
        {
            return await Task.Run(() =>
            {
                var users = userManager.Users.Include(p=>p.Photos).AsQueryable();
              return  PagedList<AppUsers>.ToPagedList(users, userReqDto.PageSize, userReqDto.PageNumber);
            });
        }

        public async Task<int> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
             AppUsers user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Id == userUpdateDto.Id);
             var userMapping = mapper.Map(userUpdateDto, user);
             appDbContext.Entry(userMapping).State = EntityState.Modified;
             return await appDbContext.SaveChangesAsync();

        }


        public int  DeleteUserAsync(string Id)
        {

          var user =  appDbContext.Users.FirstOrDefault(a => a.Id == Id);
            appDbContext.Remove(user);
          return  appDbContext.SaveChanges();

        }

        public async Task<bool> SaveChangesAsync()
        {
            return await appDbContext.SaveChangesAsync() >0;
        }

    }

}