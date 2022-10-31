namespace ChattingApp.Resource.Messager
{
    public class MailReqDto
    {
        public string MailTo { get; set; }
        public string? Subject { get; set; }
        public string Body { get; set; }
        public IList<IFormFile>? Attachments { get; set; }
    }
}
