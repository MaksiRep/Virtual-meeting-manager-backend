using DI.KC.Portal.Infrastructure.Email;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using MimeKit.Text;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.EmailService;
using RU.NSU.FIT.VirtualMeetingManager.Application.Settings;

namespace RU.NSU.FIT.VirtualMeetingManager.Email;

public class EmailService : IEmailService
{
    private readonly string _senderName;
    private readonly string _senderEmail;
    private readonly string _senderPassword;
    private readonly string _smtpServerHost;
    private readonly int _smtpServerPort;

    private readonly string _resetPassPageUrl;
    private readonly string _forgotPassPageUrl;

    public EmailService(EmailServiceSettings settings, FrontendSettings frontendSettings)
    {
        _senderName = settings.Name;
        _senderEmail = settings.Email;
        _senderPassword = settings.Password;
        _smtpServerHost = settings.SmtpHost;
        _smtpServerPort = settings.SmtpPort;

        _resetPassPageUrl = frontendSettings.ResetPassUrl;
        _forgotPassPageUrl = frontendSettings.ForgotPassUrl;
    }

    public async Task SendEmailAsync(string email, string subject, string message, CancellationToken cancellationToken)
    {
        var emailMessage = new MimeMessage();
        
        emailMessage.From.Add(new MailboxAddress(_senderName, _senderEmail));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = message
        };

        using (var smtpClient = new SmtpClient())
        {
            await smtpClient.ConnectAsync(_smtpServerHost, _smtpServerPort, true, cancellationToken);
            await smtpClient.AuthenticateAsync(_senderEmail, _senderPassword, cancellationToken);
            
            await smtpClient.SendAsync(emailMessage, cancellationToken);
            
            await smtpClient.DisconnectAsync(true, cancellationToken);
        }
    }

    public Task SendForgotPasswordEmailAsync(string email, string resetToken, Guid userId, CancellationToken cancellationToken = default)
    {
        var callbackUrl = QueryHelpers.AddQueryString(_resetPassPageUrl, new Dictionary<string, string?>
        {
            {"token", resetToken},
            {"userId", userId.ToString()}
        });
        
        const string subject = "Virtual Meeting Manager - Сброс пароля";
        var forgotMessage = $"Здравствуйте! Пожалуйста, используйте <a href=\"{callbackUrl}\">данную ссылку</a>, чтобы сбросить пароль от аккаунта VMM.";

        return SendEmailAsync(email, subject, forgotMessage, cancellationToken);
    }

    public Task SendPasswordHasBeenResetEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        const string subject = "Virtual Meeting Manager - Сброс пароля";
        var passwordHasBeenResetMessage = $"Здравствуйте! Ваш пароль был сброшен. Если это сделали не Вы, пожалуйста, используйте <a href=\"{_forgotPassPageUrl}\">данную ссылку</a>, чтобы сбросить пароль еще раз.";
        
        return SendEmailAsync(email, subject, passwordHasBeenResetMessage, cancellationToken);
    }
}