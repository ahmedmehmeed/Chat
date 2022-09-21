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
               var Password= $"{User.UserName}@123";
                User.PasswordHash = Password;
              await  userManager.CreateAsync(User, Password);
            }
        }
    }
}
