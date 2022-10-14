using ChattingApp.Extensions;
using ChattingApp.Helper.Security.Tokens;
using Microsoft.AspNetCore.Identity;

namespace ChattingApp.Domain.Models
{
    public class AppUsers : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string? KnownAs { get; set; }
        public string? Gender  { get; set; }
        public string? Introduction  { get; set; }
        public string? lookingFor  { get; set; }
        public string? Interests  { get; set; }
        public string? City  { get; set; }
        public string? Country  { get; set; }
        public ICollection<Photo>? Photos  { get; private set; } = new HashSet<Photo>();
        public ICollection<UserFollow>? Followers  { get; private set; } = new HashSet<UserFollow>();
        public ICollection<UserFollow>? Followees { get; private set; } = new HashSet<UserFollow>();
       
        // list of refresh token but one only active 
        public ICollection<RefreshToken>? RefreshTokens { get; private set; } = new HashSet<RefreshToken>();

    }
}
