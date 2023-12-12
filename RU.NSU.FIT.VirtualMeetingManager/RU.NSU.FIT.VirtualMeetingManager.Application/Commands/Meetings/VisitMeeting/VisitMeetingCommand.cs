using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.VisitMeeting;

public class VisitMeetingCommand : IRequest
{
    public int MeetingId { get; init; }
    
    [UsedImplicitly]
    public class VisitMeetingCommandHandler : IRequestHandler<VisitMeetingCommand>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;

        public VisitMeetingCommandHandler(IVMMDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task Handle(VisitMeetingCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == _currentUser.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, "Текущий пользователь не найден в системе");
            
            var meeting = await _dbContext.Meetings
                .Include(m => m.Users)
                .FirstOrDefaultAsync(m => m.Id == request.MeetingId, cancellationToken);
            
            EntityNotFoundException.ThrowIfNull(meeting, "Не найдено мероприятие с Id={0}", request.MeetingId);
            
            
            if (meeting.Users.Any(u => u.Id == user.Id))
            {
               return;
            }

            meeting.Users.Add(user);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}