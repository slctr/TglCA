using System.Net.Mail;
using MailKit.Security;
using MimeKit;
using TglCA.Bll.Interfaces.Interfaces.EmailService;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace TglCA.Bll.Services.Mail
{
    public class MailKitEmailService : IEmailService
    {
        private readonly string _senderEmail;
        private readonly string _senderPassword;

        public MailKitEmailService(string sender, string password)
        {
            _senderEmail = sender;
            _senderPassword = password;
        }
        public async Task<string> SendMailAsync(MailAddress to, string message, string subject)
        {
            MimeMessage mimeMessage = CreateMessage(to, message, subject);
            using (SmtpClient smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_senderEmail,_senderPassword);
                var result = await smtpClient.SendAsync(mimeMessage);
                await smtpClient.DisconnectAsync(true);
                return result;
            }
        }

        public async Task<string> SendConfirmationMessage(MailAddress to, string confirmationEndpoint)
        {
            string messageText = 
            $@"Welcome to E-Crypto, {to.DisplayName}

Please verify you email address.

Click the link below to verify you email address:

{confirmationEndpoint}

Thanks,
The E-Crypto Team

This is an automatically generated message. Please do not reply.";

            return await SendMailAsync(to, messageText, "Email confirmation");
        }

        private MimeMessage CreateMessage(MailAddress to, string message, string subject)
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("E-Crypto", _senderEmail));
            mimeMessage.To.Add(new MailboxAddress(to.DisplayName, to.Address));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain")
            {
                Text = message
            };
            return mimeMessage;
        }
    }
}
