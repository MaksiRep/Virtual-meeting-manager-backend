namespace RU.NSU.FIT.VirtualMeetingManager.Application.Common.Pagination;

public interface IPagedListRequest
{
    int Skip { get; init; }
    int Take { get; init; }
}