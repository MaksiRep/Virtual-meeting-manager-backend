using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;
using GenderTypeDto = RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base.GenderType;
using GenderType = RU.NSU.FIT.VirtualManager.Domain.ValueTypes.GenderType;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.UpdateMeeting;

public class UpdateMeetingCommand : IRequest, IEditMeetingCommand
{
    public int Id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }
    
    public int? MaxUsers { get; init; }

    public int? MinAge { get; init; }

    public GenderTypeDto? Gender { get; init; }

    public string? ShortDescription { get; init; }

    [UsedImplicitly]
    public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;

        private readonly EditMeetingCommandValidator _commandValidator;

        public UpdateMeetingCommandHandler(
            IVMMDbContext dbContext, 
            ICurrentUser currentUser,
            EditMeetingCommandValidator commandValidator)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
            _commandValidator = commandValidator;
        }

        public async Task Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == _currentUser.Id, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, "Текущий пользователь не найден в системе");
            
            var meeting = await _dbContext.Meetings
                .Include(m => m.Manager)
                .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
            
            EntityNotFoundException.ThrowIfNull(meeting, "Не найдено мероприятие с Id={0}", request.Id);

            if (meeting.Manager.Id != user.Id && _currentUser.CanEditAllMeetings() is false)
            {
                throw new ForbiddenException("Текущий пользователь не имеет прав на изменение данного мероприятия");
            }
            
            // Проверяем команду
            _commandValidator.ValidateAndThrow(request);

            meeting.UpdateMeeting(request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.MaxUsers,
                request.MinAge,
                (GenderType?)request.Gender,
                request.ShortDescription);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}