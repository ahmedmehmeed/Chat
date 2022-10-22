using ChattingApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace ChattingApp.Persistence.DataSeed
{
    public class SeedData
    {

        public static async Task SeedUserData(UserManager<AppUsers> userManager)
        {
            var UsersData = System.IO.File.ReadAllText("Persistence/DataSeed/UsersData.json");
            var Users = JsonSerializer.Deserialize<List<AppUsers>>(UsersData);
            foreach (var User in Users)
            {
                var Password = $"{User.UserName}@123";
                User.PasswordHash = Password;
                await userManager.CreateAsync(User, Password);
            }


        }

        public static async Task AddRoleToUsers(UserManager<AppUsers> userManager)
        {
            var users = userManager.Users.ToList();
            foreach (var user in users)
            {
                await userManager.AddToRoleAsync(user,"User");
            }

            //var admin = new AppUsers
            //{
            //    UserName = "Admin",
            //    Email = "Admin@gmail.com",
            //    PasswordHash = "Admin@123"
            //};
            //await userManager.CreateAsync(admin, "Admin@123");
        }
    }
}
