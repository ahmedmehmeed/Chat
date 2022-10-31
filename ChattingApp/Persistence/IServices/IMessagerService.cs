namespace ChattingApp.Persistence.IServices
{
    public interface IMessagerService
    {
        Task SendMailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null);
        Task SendAccountVerificationEmail(string mailTo);
    }
}
