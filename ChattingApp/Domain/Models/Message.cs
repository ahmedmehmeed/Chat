namespace ChattingApp.Domain.Models
{
    public class Message
    {
        public Message()
        {
           
        }
        public Guid Id { get; set; }= Guid.NewGuid();

        public string? SenderId { get; set; }
        public string? SenderUsername { get; set; }

        public AppUsers Sender { get; set; }

        public string? ReceiverId { get; set; }
        public string? ReceiverUsername { get; set; }

        public AppUsers Receiver { get; set; } 

        public string? Content { get; set; }

        public DateTime? DateRead { get; set; }
        public DateTime DateMessageSent { get; set; } = DateTime.Now;

        public bool SenderDeleted { get; set; }
        public bool RecieverDeleted { get; set; }

    }
}
