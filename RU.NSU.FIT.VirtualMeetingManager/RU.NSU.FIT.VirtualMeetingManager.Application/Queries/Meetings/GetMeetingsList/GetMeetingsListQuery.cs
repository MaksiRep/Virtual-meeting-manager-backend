using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Common.Pagination;
using RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;
using GenderType = RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base.GenderType;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingsList;

public class GetMeetingsListQuery : IRequest<GetMeetingsListResponse>, IPagedListRequest
{
    public int Skip { get; init; }
    public int Take { get; init; }

    public int? MinAge { get; init; }

    public DateTime? StartDate { get; init; }

    public DateTime? EndDate { get; init; }

    public GenderType? GenderType { get; init; }

    [UsedImplicitly]
    public class GetMeetingsListQueryHandler : IRequestHandler<GetMeetingsListQuery, GetMeetingsListResponse>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _iCurrentUser;

        public GetMeetingsListQueryHandler(IVMMDbContext dbContext, ICurrentUser iCurrentUser)
        {
            _dbContext = dbContext;
            _iCurrentUser = iCurrentUser;
        }

        public async Task<GetMeetingsListResponse> Handle(GetMeetingsListQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == _iCurrentUser.Id, cancellationToken);
            EntityNotFoundException.ThrowIfNull(user, "Текущий пользователь не найден в системе");

            var query = _dbContext.Meetings
                .Include(m => m.Users)
                .FilterByMinAge(request.MinAge)
                .FilterByDates(request.StartDate, request.EndDate)
                .FilterByGender(request.GenderType);

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
                    IsUserVisitMeeting = m.Users.Contains(user) || m.Manager.Id.Equals(user.Id)
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