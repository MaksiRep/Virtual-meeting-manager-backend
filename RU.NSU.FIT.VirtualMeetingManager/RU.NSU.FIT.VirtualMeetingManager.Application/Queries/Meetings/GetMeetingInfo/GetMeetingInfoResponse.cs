using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingInfo;

public class GetMeetingInfoResponse
{
    
    public int Id { get; init; }
    
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public DateTime StartDate { get; init; }
    
    public DateTime EndDate { get; init; }
    
    public int UsersCount { get; init; }

    public string ManagerFirstName { get; init; }
    public string ManagerLastName { get; init; }
    public string ManagerEmail { get; init; }
    public string? ManagerPhone { get; init; }
    
    public Guid ManagerId { get; init; }
    
    public bool IsUserVisitMeeting { get; init; }
    
    public int? MaxUsers { get; init; }
    
    public int? MinAge { get; init; }
    
    public GenderType? Gender { get; init; }
}