using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;
using GenderTypeDto = RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base.GenderType;
using GenderType = RU.NSU.FIT.VirtualManager.Domain.ValueTypes.GenderType;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.UpdateMeetingImage;

public class UpdateMeetingImageCommand : IRequest
{
    public int MeetingId { get; init; }

    public string? ImageUrl { get; init; }

    [UsedImplicitly]
    public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingImageCommand>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;

        public UpdateMeetingCommandHandler(IVMMDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task Handle(UpdateMeetingImageCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == _currentUser.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, "Текущий пользователь не найден в системе");
            
            var meeting = await _dbContext.Meetings
                .Include(m => m.Manager)
                .FirstOrDefaultAsync(m => m.Id == request.MeetingId, cancellationToken);
            
            EntityNotFoundException.ThrowIfNull(meeting, "Не найдено мероприятие с Id={0}", request.MeetingId);

            if (meeting.Manager.Id != user.Id && _currentUser.CanEditAllMeetings() is false)
            {
                throw new ForbiddenException("Текущий пользователь не имеет прав на изменение данного мероприятия");
            }

            meeting.UpdateImageUrl(request.ImageUrl);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}