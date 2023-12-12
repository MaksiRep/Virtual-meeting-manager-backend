using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.DeleteMeeting;

public class DeleteMeetingCommand : IRequest
{
    public int MeetingId { get; init; }
    
    public sealed class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;

        public DeleteMeetingCommandHandler(IVMMDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == _currentUser.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, "Текущий пользователь не найден в системе");
            
            var meeting = await _dbContext.Meetings
                .Include(m => m.Manager)
                .FirstOrDefaultAsync(m => m.Id == request.MeetingId, cancellationToken);
            
            EntityNotFoundException.ThrowIfNull(meeting, "Не найдено мероприятие с Id={0}", request.MeetingId);

            if (meeting.Manager.Id != user.Id && _currentUser.CanDeleteAllMeetings() is false)
            {
                throw new ForbiddenException("Текущий пользователь не имеет прав на удаление данного мероприятия");
            }

            _dbContext.Meetings.Remove(meeting);

            await _dbContext.SaveChangesAsync(cancellationToken);
            
        }
    }
}