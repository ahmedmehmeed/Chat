
using ChattingApp.Persistence;
using ChattingApp.Persistence.Interface;
using ChattingApp.Persistence.Interfaces;
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

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
