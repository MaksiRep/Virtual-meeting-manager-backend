namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingsList;

public record MeetingListItemDto
{
    public int Id { get; init; }

    public string Name { get; init; }

    public DateTime StartDate { get; init; }
    
    public DateTime EndDate { get; init; }
    public string? ShortDescription { get; init; }
    
    public bool IsUserVisitMeeting { get; init; }
}