using ChattingApp.Domain;

namespace ChattingApp.Resource.User
{
    public class PhotoDto:BaseResponse
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }
    }
}