namespace ChattingApp.Resource.User
{
    public class Uploadphoto
    {
        public  string? UserId { get; set; }
        public bool IsMain { get; set; }
        public IFormFile? photo { get; set; }
    }
}
