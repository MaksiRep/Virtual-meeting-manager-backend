namespace RU.NSU.FIT.VirtualMeetingManager.Application.Common.Pagination;

public interface IPagedListResponse<TItem>
{
    IList<TItem> Items { get; init; }
    int TotalCount { get; init; }
}