using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.CancelMeetingVisiting;

public class CancelMeetingVisitingCommand : IRequest
{
    public int MeetingId { get; init; }

    [UsedImplicitly]
    public class CancelMeetingVisitingCommandHandler : IRequestHandler<CancelMeetingVisitingCommand>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;

        public CancelMeetingVisitingCommandHandler(IVMMDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task Handle(CancelMeetingVisitingCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == _currentUser.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, "Текущий пользователь не найден в системе");

            var meeting = await _dbContext.Meetings
                .Include(m => m.Users)
                .FirstOrDefaultAsync(m => m.Id == request.MeetingId, cancellationToken);

            EntityNotFoundException.ThrowIfNull(meeting, "Не найдено мероприятие с Id={0}", request.MeetingId);

            if (!meeting.Users.Contains(user))
            {
                throw new BadRequestException("Текущий пользователь не является участником мероприятия");
            }

            meeting.Users.Remove(user);


            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}