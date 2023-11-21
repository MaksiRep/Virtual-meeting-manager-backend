using RU.NSU.FIT.VirtualMeetingManager.Application.Common.Pagination;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.GetUsers;

public class GetUserListResponse : IPagedListResponse<UserDto>
{
    public IList<UserDto> Items { get; init; }
    public int TotalCount { get; init; }
}