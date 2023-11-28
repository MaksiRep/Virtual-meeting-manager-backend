using RU.NSU.FIT.VirtualMeetingManager.Application.Common.Pagination;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingsList;

public class GetMeetingsListResponse : IPagedListResponse<MeetingListItemDto>
{
    public IList<MeetingListItemDto> Items { get; init; }
    public int TotalCount { get; init; }
}