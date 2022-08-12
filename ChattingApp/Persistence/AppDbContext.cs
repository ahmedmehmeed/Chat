using ChattingApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChattingApp.Controllers___Copy__2_
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUsers> Users { get; set; }
    }
}
