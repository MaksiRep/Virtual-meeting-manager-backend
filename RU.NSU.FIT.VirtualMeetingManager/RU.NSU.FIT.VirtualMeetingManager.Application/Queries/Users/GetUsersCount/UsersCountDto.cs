using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualMeetingManager.Application.Common.Pagination;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetUsersCount;

public record UsersCountDto 
{
    public int TotalCount { get; init; }
}