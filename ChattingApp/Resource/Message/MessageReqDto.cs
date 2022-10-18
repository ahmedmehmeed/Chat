using ChattingApp.Resource.Pagination;

namespace ChattingApp.Resource.Message
{
    public class MessageReqDto:BasePaginationDto
    {
        public string? ReceiverUsername { get; set; }
        public string? SenderUsername { get; set; }

        public string? Container { get; set; } = "Unread"; 
    }
}
