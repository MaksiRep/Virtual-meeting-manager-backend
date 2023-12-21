using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.Base;

public interface IEditMeetingCommand
{
    public string Name { get; init; }

    public string Description { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }
    
    public int? MaxUsers { get; init; }

    public int? MinAge { get; init; }

    public GenderType? Gender { get; init; }

    public string? ShortDescription { get; init; }
}