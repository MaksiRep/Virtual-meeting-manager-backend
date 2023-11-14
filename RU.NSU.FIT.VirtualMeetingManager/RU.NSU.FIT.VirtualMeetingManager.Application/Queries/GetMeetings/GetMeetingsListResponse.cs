using RU.NSU.FIT.VirtualMeetingManager.Application.Common.Pagination;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.GetMeetings;

public class GetMeetingsListResponse : IPagedListResponse<MeetingDto>
{
    public IList<MeetingDto> Items { get; init; }
    public int TotalCount { get; init; }
}