using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Mail;

namespace LeaveManagement.Web.Services
{
    public class EmailSender : IEmailSender
    {
        private string smtpServer;
        private int smtpPort;
        private string fromEmailAddress;
        private readonly ILogger<EmailSender> logger;

        public EmailSender(string smtpServer, int smtpPort, string fromEmailAddress, 
            ILogger<EmailSender>? logger = null)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.fromEmailAddress = fromEmailAddress;
            this.logger = logger ?? NullLogger<EmailSender>.Instance;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(fromEmailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));
            try
            {

            //initialize client
            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.Send(message);
            }
            } catch (Exception ex)
            {
                logger.LogInformation("Error sending emailim Email Sender", ex);
            }

            return Task.CompletedTask;
        }
    }
}
