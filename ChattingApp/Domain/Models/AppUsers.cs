using ChattingApp.Helper.Security.Tokens;
using Microsoft.AspNetCore.Identity;

namespace ChattingApp.Domain.Models
{
    public class AppUsers : IdentityUser
    {
        //public int Id { get; set; }
        //public string? Username { get; set; }
        //public byte[]? PasswordHash { get; set; }
        //public byte[]? PasswordSalt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        // list of refresh token but one only active 
        public List<RefreshToken>? RefreshTokens { get; set; }

    }
}
