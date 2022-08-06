using ChattingApp.Controllers___Copy__2_;
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



        }
    }
}
