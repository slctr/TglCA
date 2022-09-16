using System.Net.Mail;

namespace TglCA.Bll.Interfaces.Interfaces.EmailService;

public interface IEmailService
{
    public Task<string> SendMailAsync(MailAddress to, string message, string subject);
    public Task<string> SendConfirmationMessage(MailAddress to, string confirmationEndpoint);
}