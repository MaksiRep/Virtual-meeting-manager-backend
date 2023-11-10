using RU.NSU.FIT.VirtualManager.Domain;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Common;

/// <inheritdoc />
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}