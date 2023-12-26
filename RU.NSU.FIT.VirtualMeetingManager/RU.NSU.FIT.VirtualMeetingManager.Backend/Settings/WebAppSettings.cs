using DI.KC.Portal.Infrastructure.Database.Settings;
using DI.KC.Portal.Infrastructure.Email;
using RU.NSU.FIT.VirtualMeetingManager.Application.Settings;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Settings;

/// <summary>
/// Настройки приложения
/// </summary>
public sealed record WebAppSettings
{
    /// <summary>
    /// Настройки БД
    /// </summary>
    public DatabaseSettings? Database { get; init; }
    /// <summary>
    /// Настройки аутентификации
    /// </summary>
    public AuthSettings? Auth { get; init; }
    
    /// <summary>
    /// Настройки сервиса Email-рассылок
    /// </summary>
    public EmailServiceSettings? EmailService { get; init; }
    
    /// <summary>
    /// Информация о Frontend-приложении
    /// </summary>
    public FrontendSettings? Frontend { get; init; }
}