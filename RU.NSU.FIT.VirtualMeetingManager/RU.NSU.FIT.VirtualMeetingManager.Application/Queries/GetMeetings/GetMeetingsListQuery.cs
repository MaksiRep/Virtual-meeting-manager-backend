using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualMeetingManager.Application.Common.Pagination;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.GetMeetings;

public class GetMeetingsListQuery : IRequest<GetMeetingsListResponse>, IPagedListRequest
{
    public int Skip { get; init; }
    public int Take { get; init; }

    public class GetMeetingsListQueryHandler : IRequestHandler<GetMeetingsListQuery, GetMeetingsListResponse>
    {
        private readonly IVMMDbContext _dbContext;

        public GetMeetingsListQueryHandler(IVMMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetMeetingsListResponse> Handle(GetMeetingsListQuery request,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Meetings
                .AsQueryable();
            var totalCount = await query.CountAsync(cancellationToken);
            var result = await query
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(m => new MeetingListItemDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    ImageUrl = m.ImageUrl,
                })
                .ToListAsync(cancellationToken);
            return new GetMeetingsListResponse()
            {
                Items = result,
                TotalCount = totalCount
            };
        }
    }
}