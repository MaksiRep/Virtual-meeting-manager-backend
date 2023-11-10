namespace RU.NSU.FIT.VirtualManager.Domain;

/// <summary>
/// Предоставляет информацию по текущим дате и времени
/// </summary>
public interface IDateTimeProvider
{
    
    DateTime UtcNow { get; }
}