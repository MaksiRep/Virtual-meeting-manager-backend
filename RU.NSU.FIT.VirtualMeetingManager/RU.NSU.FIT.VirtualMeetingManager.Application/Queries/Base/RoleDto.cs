namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;

public record RoleDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public RoleType Type { get; init; }
}