namespace RU.NSU.FIT.VirtualMeetingManager.Application.Settings;

/// <summary>
/// Настройки аутентификации
/// </summary>
public sealed record AuthSettings
{
    /// <summary>
    /// Ключ подписи токенов
    /// </summary>
    public string? SecretKey { get; init; }
    /// <summary>
    /// Время жизни access токена
    /// </summary>
    public int AccessTokenLifetimeMinutes { get; init; }
    /// <summary>
    /// Время жизни refresh токена
    /// </summary>
    public int RefreshTokenLifetimeDays { get; init; }
}