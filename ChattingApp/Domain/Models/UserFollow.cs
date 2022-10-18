namespace ChattingApp.Domain.Models
{
    public class UserFollow
    {
        public string? SourceUserId { get; set; }
        public AppUsers? SourceUser { get; set; } 
        public string? UserFollowedId { get; set; }
        public AppUsers? UserFollowed { get; set; } 
    }
}
