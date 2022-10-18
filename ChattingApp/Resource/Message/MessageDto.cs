namespace ChattingApp.Resource.Message
{
    public class MessageDto
    {
        public Guid Id { get; set; } 
        public string? SenderId { get; set; }
        public string? SenderUsername { get; set; }
        public string? SenderPhotoURL { get; set; }
        public string? ReceiverId { get; set; }
        public string? ReceiverUsername { get; set; }
        public string? ReceiverPhotoURL { get; set; }
        public string? Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime? DateMessageSent { get; set; }

    }
} 
 