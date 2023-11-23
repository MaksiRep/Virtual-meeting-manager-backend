using RU.NSU.FIT.VirtualManager.Domain.Entities;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.GetUsers;

public record UserDto
{
    public Guid Id {get; init;}
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public DateOnly BirthDate { get; init; }
    public GenderType Gender { get; init; }
    
}