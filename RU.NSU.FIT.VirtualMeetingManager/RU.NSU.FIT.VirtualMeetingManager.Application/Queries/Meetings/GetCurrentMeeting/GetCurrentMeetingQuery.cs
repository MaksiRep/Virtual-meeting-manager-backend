using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetCurrentMeeting;

public class GetCurrentMeetingQuery : IRequest<MeetingResponse>
{
    public int MeetingId { get; init; }

    [UsedImplicitly]
    public sealed class GetCurrentMeetingQueryHandler : IRequestHandler<GetCurrentMeetingQuery, MeetingResponse>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;

        public GetCurrentMeetingQueryHandler(IVMMDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task<MeetingResponse> Handle(GetCurrentMeetingQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == _currentUser.Id, cancellationToken);
            EntityNotFoundException.ThrowIfNull(user, "Текущий пользователь не найден в системе");

            var meeting = await _dbContext.Meetings
                .Include(m => m.Users)
                .Include(m => m.Manager)
                .FirstOrDefaultAsync(m => m.Id == request.MeetingId, cancellationToken);

            EntityNotFoundException.ThrowIfNull(meeting, "Не найдено мероприятие с Id={0}", request.MeetingId);
            
            var usersCount = meeting.Users.Count;

            var isUserVisitMeeting = meeting.Users.Contains(user) || meeting.Manager.Id.Equals(user.Id);
            
            return new MeetingResponse
            {
                Id = meeting.Id,
                Name = meeting.Name,
                Description = meeting.Description,
                StartDate = meeting.StartDate,
                EndDate = meeting.EndDate,
                usersCount = usersCount,
                ManagerEmail = meeting.Manager.Email,
                ManagerId = meeting.Manager.Id,
                IsUserVisitMeeting = isUserVisitMeeting
            };
        }
    }
}