using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Base.EditUserCommand;

public interface IEditUserCommand
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public GenderType Gender { get; init; }
    public DateOnly BirthDate { get; init; }
    public string? Phone { get; init; }
}