using MediatR;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;
using GenderType = RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base.GenderType;
using GenderTypeDto = RU.NSU.FIT.VirtualManager.Domain.ValueTypes.GenderType;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meeting.UpdateMeeting;

public class UpdateMeetingCommand : IRequest
{
    public int id { get; init; }

    public string Name { get; init; }

    public string Description { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public string ImageUrl { get; init; }

    public int? MaxUsers { get; init; }

    public int? MinAge { get; init; }

    public GenderType? Gender { get; init; }

    public string ShortDescription { get; init; }

    public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;

        public UpdateMeetingCommandHandler(IVMMDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
        {
            var meeting = await _dbContext.Meetings.FindAsync(request.id);
            if (meeting == null)
            {
                throw new BadRequestException("Мероприятие не найдено");
            }

            var user = await _dbContext.Users.FindAsync(_currentUser.Id);

            if (meeting.Manager != user)
            {
                throw new BadRequestException("Пользователь не является организатором");
            }

            meeting.UpdateMeeting(request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.ImageUrl,
                request.MaxUsers,
                request.MinAge,
                (GenderTypeDto)request.Gender!,
                request.ShortDescription);


            _dbContext.Meetings.Update(meeting);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}