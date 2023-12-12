namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingsList;

public record MeetingListItemDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }

    public string ImageUrl { get; set; }
    
    public bool IsUserVisitMeeting { get; set; }
}