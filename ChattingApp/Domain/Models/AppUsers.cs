using ChattingApp.Extensions;
using ChattingApp.Helper.Security.Tokens;
using Microsoft.AspNetCore.Identity;

namespace ChattingApp.Domain.Models
{
    public class AppUsers : IdentityUser
    {
        public AppUsers()
        {
            Photos = new HashSet<Photo>();
            Followers = new HashSet<UserFollow>();
            Followees = new HashSet<UserFollow>();
            MessageRecieved = new HashSet<Message>();
            MessagesSent = new HashSet<Message>();
            RefreshTokens = new HashSet<RefreshToken>();
        }
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
        public ICollection<Photo>? Photos  { get; private set; } 
        public ICollection<UserFollow>? Followers  { get; private set; }
        public ICollection<UserFollow>? Followees { get; private set; }

        public ICollection<Message>? MessagesSent { get; private set; }
        public ICollection<Message>? MessageRecieved { get; private set; }

        // list of refresh token but one only active 
        public ICollection<RefreshToken>? RefreshTokens { get; private set; } 

    }
}
