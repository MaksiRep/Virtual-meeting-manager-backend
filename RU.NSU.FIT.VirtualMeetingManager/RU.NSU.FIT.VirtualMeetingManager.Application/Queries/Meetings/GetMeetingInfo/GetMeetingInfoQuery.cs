using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingInfo;

public class GetMeetingInfoQuery : IRequest<GetMeetingInfoResponse>
{
    public int MeetingId { get; init; }

    [UsedImplicitly]
    public sealed class GetMeetingInfoQueryHandler : IRequestHandler<GetMeetingInfoQuery, GetMeetingInfoResponse>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;

        public GetMeetingInfoQueryHandler(IVMMDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task<GetMeetingInfoResponse> Handle(GetMeetingInfoQuery request, CancellationToken cancellationToken)
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

            var isUserVisitMeeting = meeting.Users.Any(u => u.Id == user.Id) || meeting.Manager.Id.Equals(user.Id);
            
            return new GetMeetingInfoResponse
            {
                Id = meeting.Id,
                Name = meeting.Name,
                Description = meeting.Description,
                StartDate = meeting.StartDate,
                EndDate = meeting.EndDate,
                UsersCount = usersCount,
                ManagerFirstName = meeting.Manager.FirstName,
                ManagerLastName = meeting.Manager.LastName,
                ManagerEmail = meeting.Manager.Email,
                ManagerPhone = meeting.Manager.PhoneNumber,
                ManagerId = meeting.Manager.Id,
                IsUserVisitMeeting = isUserVisitMeeting
            };
        }
    }
}