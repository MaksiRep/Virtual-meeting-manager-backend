namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.GetUsers;

public record UserDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
}