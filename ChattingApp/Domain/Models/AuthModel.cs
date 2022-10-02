using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ChattingApp.Domain.Models
{
    public class AuthModel
    {
        public string UserId { get; set; }
        public String? Message { get; set; }
        public String? Username { get; set; }
        public String? Email { get; set; }
        public String? Token { get; set; }
        public List <String>? Roles { get; set; }
        public bool IsAuthencated { get; set; }
        // public DateTime TokenExpiration { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenEXpiration { get; set; }
        public bool IsSuccess { get; set; }
    }
}
