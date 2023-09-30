using ExpenSpend.Core.Emails;

namespace ExpenSpend.Service.Emails.Interfaces;

/// <summary>
/// Represents the interface for sending and managing emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends the given email message.
    /// </summary>
    /// <param name="email">The email message to send.</param>
    void SendEmail(MessageDto email);

    /// <summary>
    /// Creates an email message with a validation template and a confirmation code.
    /// </summary>
    /// <param name="email">The email address to send the email to.</param>
    /// <param name="confirmationCode">The confirmation code to include in the email.</param>
    /// <returns>A Task representing the asynchronous operation, containing the created MessageDto object.</returns>
    Task<MessageDto> CreateEmailValidationTemplateMessage(string email, string confirmationCode);

    /// <summary>
    /// Gets the email confirmation page template as a string.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation, containing the email confirmation page template string.</returns>
    Task<string> EmailConfirmationPageTemplate();

    /// <summary>
    /// Sends a password reset email to the given email address with the given reset link.
    /// </summary>
    /// <param name="recipientEmail">The email address to send the reset email to.</param>
    /// <param name="resetLink">The link to include in the reset email.</param>
    void SendPasswordResetEmail(string recipientEmail, string resetLink);

    /// <summary>
    /// Sends a notification email to the given email address and user name indicating that their password has been changed.
    /// </summary>
    /// <param name="email">The email address to send the notification email to.</param>
    /// <param name="userName">The username associated with the email address.</param>
    void SandPasswordChangeNotification(string email, string userName);
}
