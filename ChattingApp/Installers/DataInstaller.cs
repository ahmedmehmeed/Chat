
using ChattingApp.Persistence;
using ChattingApp.Persistence.IRepositories;
using ChattingApp.Persistence.IServices;
using ChattingApp.Persistence.Repositories;
using ChattingApp.Persistence.Services;
using Microsoft.EntityFrameworkCore;

namespace ChattingApp.Installers
{
    public class DataInstaller : IInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IUploadPhotoService, UploadPhotoService>();
        }
    }
}
