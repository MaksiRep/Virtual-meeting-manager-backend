using JetBrains.Annotations;
using MediatR;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.CreateMeeting;

public class CreateMeetingCommand : IRequest<CreateMeetingResponse>, IEditMeetingCommand
{
    public string Name { get; init; }

    public string Description { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }
    
    public int? MaxUsers { get; init; }

    public int? MinAge { get; init; }

    public GenderType? Gender { get; init; }

    public string? ShortDescription { get; init; }

    [UsedImplicitly]
    public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, CreateMeetingResponse>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;

        private readonly EditMeetingCommandValidator _commandValidator;

        public CreateMeetingCommandHandler(
            IVMMDbContext dbContext, 
            ICurrentUser currentUser, 
            EditMeetingCommandValidator commandValidator)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
            _commandValidator = commandValidator;
        }

        public async Task<CreateMeetingResponse> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(_currentUser.Id);
            
            EntityNotFoundException.ThrowIfNull(user, "Текущий пользователь не найден в системе");
            
            if (_currentUser.CanCreateMeeting() is false)
            {
                throw new ForbiddenException("Текущий пользователь не имеет прав на создание мероприятий");
            }
            
            // Проверяем команду
            _commandValidator.ValidateAndThrow(request);

            var newMeeting = new Meeting
            (
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.MaxUsers,
                request.MinAge,
                (VirtualManager.Domain.ValueTypes.GenderType?) request.Gender,
                user,
                request.ShortDescription
            );
            
            _dbContext.Meetings.Add(newMeeting);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateMeetingResponse
            {
                MeetingId = newMeeting.Id
            };
        }
    }
}