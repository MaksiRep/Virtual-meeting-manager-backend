using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetCurrentUser;

public record CurrentUserDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public IList<RoleType> Roles { get; init; } = Array.Empty<RoleType>();
}