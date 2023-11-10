﻿namespace DI.KC.Portal.Infrastructure.Database.Settings;

/// <summary>
/// Настройки БД
/// </summary>
public sealed record DatabaseSettings
{
    /// <summary>
    /// Строка подключения к БД
    /// </summary>
    public string? ConnectionString { get; set; }
}