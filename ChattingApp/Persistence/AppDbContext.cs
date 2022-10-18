using ChattingApp.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChattingApp.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUsers>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
           
        }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<UserFollow> Follows { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserFollow>()
                   .HasKey(k => new { k.SourceUserId, k.UserFollowedId });

            builder.Entity<UserFollow>()
                   .HasOne(s => s.SourceUser)
                   .WithMany(f => f.Followers)
                   .HasForeignKey(s => s.SourceUserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserFollow>()
                   .HasOne(s => s.UserFollowed)
                   .WithMany(f => f.Followees)
                   .HasForeignKey(s => s.UserFollowedId)
                   .OnDelete(DeleteBehavior.NoAction);

           builder.Entity<Message>()
                   .HasOne(s => s.Sender)
                   .WithMany(f => f.MessagesSent)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Message>()
                   .HasOne(s => s.Receiver)
                   .WithMany(f => f.MessageRecieved)
                   .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
