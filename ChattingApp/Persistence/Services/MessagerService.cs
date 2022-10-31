using ChattingApp.Domain.Models;
using ChattingApp.Helper.Third_Party_Settings;
using ChattingApp.Persistence.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ChattingApp.Persistence.Services
{
    public class MessagerService : IMessagerService
    {
        private readonly SmtpSettings smtpSettings;
        private readonly UserManager<AppUsers> userManager;

        public MessagerService(IOptions<SmtpSettings> SmtpSettings, UserManager<AppUsers> userManager)
        {
            smtpSettings = SmtpSettings.Value;
            this.userManager = userManager;
        }


        public async Task SendMailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(smtpSettings.Email),
                Subject = subject
            };

            email.To.Add(MailboxAddress.Parse(mailTo));
            var builder = new BodyBuilder();

            if(attachments != null)
            {
                byte[] filesBytes;

                foreach (var file in attachments)
                {
                    if (file.Length > 0)
                    {
                       using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        filesBytes = ms.ToArray();
                        builder.Attachments.Add(file.FileName, filesBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(smtpSettings.DisplayName, smtpSettings.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(smtpSettings.Host, smtpSettings.Port, SecureSocketOptions.StartTls);
            // y should create app password to your email not your email password
            // "https://myaccount.google.com/apppasswords?rapt=AEjHL4P9HwaEZHDlj-WBEl2DWoZ4RwFLoPW37Sp-PKvevuXbsxyBUVhoy28Tcg2VY68L0roAgs_PrV_KN-GmwUoQuQJ_W_QAqQ"
            smtp.Authenticate(smtpSettings.Email, smtpSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public Task SendAccountVerificationEmail(string mailTo)
        {
            throw new NotImplementedException();
        }

    }
}
