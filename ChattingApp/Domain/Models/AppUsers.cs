using ChattingApp.Extensions;
using ChattingApp.Helper.Security.Tokens;
using Microsoft.AspNetCore.Identity;

namespace ChattingApp.Domain.Models
{
    public class AppUsers : IdentityUser
    {
    
        //public string? Username { get; set; }
        //public byte[]? PasswordHash { get; set; }
        //public byte[]? PasswordSalt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime Created { get; set; }=DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;

        public string? KnownAs { get; set; }
        public string? Gender  { get; set; }
        public string? Introduction  { get; set; }
        public string? lookingFor  { get; set; }
        public string? Interests  { get; set; }
        public string? City  { get; set; }
        public string? Country  { get; set; }
        public ICollection<Photo>? Photos  { get; set; }
        // list of refresh token but one only active 
        public List<RefreshToken>? RefreshTokens { get; set; }

    }
}
