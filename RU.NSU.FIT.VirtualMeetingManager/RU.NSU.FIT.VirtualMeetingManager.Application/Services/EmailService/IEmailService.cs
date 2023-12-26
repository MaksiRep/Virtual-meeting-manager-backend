namespace RU.NSU.FIT.VirtualMeetingManager.Application.Services.EmailService;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message, CancellationToken cancellationToken = default);

    Task SendForgotPasswordEmailAsync(string email, string resetToken, Guid userId, CancellationToken cancellationToken = default);

    Task SendPasswordHasBeenResetEmailAsync(string email, CancellationToken cancellationToken = default);
}