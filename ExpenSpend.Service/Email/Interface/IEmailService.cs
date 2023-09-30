using ExpenSpend.Core.Email;

namespace ExpenSpend.Service.Email.Interface;

public interface IEmailService
{
    void SendEmail(MessageDto email);
    Task<MessageDto> CreateEmailValidationTemplateMessage(string email, string confirmationCode);
    Task<string> EmailConfirmationPageTemplate();
    void SendPasswordResetEmail(string recipientEmail, string resetLink);
    void SandPasswordChangeNotification(string email, string userName);
}
