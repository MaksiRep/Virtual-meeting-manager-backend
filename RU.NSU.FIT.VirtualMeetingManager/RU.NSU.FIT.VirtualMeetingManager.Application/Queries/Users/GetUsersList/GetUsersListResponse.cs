using RU.NSU.FIT.VirtualMeetingManager.Application.Common.Pagination;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetUsersList;

public class GetUserListResponse : IPagedListResponse<UserDto>
{
    public IList<UserDto> Items { get; init; }
    public int TotalCount { get; init; }
}