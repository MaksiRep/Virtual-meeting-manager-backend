namespace DI.KC.Portal.Infrastructure.Email;

/// <summary>
/// Настройки сервиса Email-рассылок
/// </summary>
public sealed record EmailServiceSettings
{
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string SmtpHost { get; init; } = null!;
    public int SmtpPort { get; init; }
}