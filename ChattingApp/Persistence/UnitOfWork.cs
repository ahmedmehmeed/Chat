using AutoMapper;
using ChattingApp.Domain.Models;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ChattingApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMapper mapper;
        private readonly UserManager<AppUsers> userManager;
        private readonly AppDbContext appDbContext;

        public UnitOfWork(IMapper mapper,UserManager<AppUsers> userManager , AppDbContext appDbContext)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.appDbContext = appDbContext;
        }

        public IUserRepository UserRepository => new UserRepository(appDbContext, userManager,mapper);

        public IFollowsRepository FollowsRepository =>  new FollowsRepository(appDbContext, mapper);

        public IMessageRepository MessageRepository =>  new MessageRepository(appDbContext, mapper);


        public bool HasChanges()
        {
            return appDbContext.ChangeTracker.HasChanges();
        }

        public async Task<bool> Commit()
        {
            return await appDbContext.SaveChangesAsync() > 0;
        }
    }
}
