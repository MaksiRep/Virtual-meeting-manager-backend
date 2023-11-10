using DI.KC.Portal.Infrastructure.Database.Settings;
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
}