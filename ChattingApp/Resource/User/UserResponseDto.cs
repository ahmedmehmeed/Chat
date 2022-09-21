using ChattingApp.Helper.Security.Tokens;

namespace ChattingApp.Resource.User
{
    public class UserResponseDto
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int age { get; set; }
        public string PhoneURL { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public string? KnownAs { get; set; }
        public string? Gender { get; set; }
        public string? Introduction { get; set; }
        public string? lookingFor { get; set; }
        public string? Interests { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public ICollection<PhotoDto>? PhotoDto { get; set; }
        // list of refresh token but one only active 
        public List<RefreshToken>? RefreshTokens { get; set; }

    }
}
