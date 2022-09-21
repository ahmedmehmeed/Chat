using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChattingApp.Domain.Models;
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

        public async Task<IQueryable<AppUsers>> GetUsersAsync()
        {
            return await Task.Run(() =>
            {
                return userManager.Users.Include(p=>p.Photos).AsQueryable();
            });
        }

        public void UpdateUserAsync(string Id, AppUsers appUsers)
        {
            appDbContext.Entry(appUsers).State = EntityState.Modified;
        }

        public   void DeleteUserAsync(string Id)
        {

          var user =  appDbContext.Users.FirstOrDefault(a => a.Id == Id);
            appDbContext.Remove(user);
            appDbContext.SaveChanges();

        }

        public async Task<bool> SaveChangesAsync()
        {
            return await appDbContext.SaveChangesAsync() >0;
        }

    }

}