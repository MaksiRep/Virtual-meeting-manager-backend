namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingInfo;

public class MeetingResponse
{
    
    public int Id { get; init; }
    
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public DateTime StartDate { get; init; }
    
    public DateTime EndDate { get; init; }
    
    public int UsersCount { get; init; }
    
    public string ManagerEmail { get; init; }
    
    public Guid ManagerId { get; init; }
    
    public bool IsUserVisitMeeting { get; init; }
    
}