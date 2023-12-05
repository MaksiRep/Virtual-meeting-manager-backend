using MediatR;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meeting.CreateMeeting;

public class CreateMeetingCommand : IRequest
{
    public MeetingDto MeetingDto { get; init; }

    public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;
        
        public CreateMeetingCommandHandler(IVMMDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
        {
            var meetingDto = request.MeetingDto;
            var user = await _dbContext.Users.FindAsync(_currentUser.Id);
            _dbContext.Meetings.Add(new global::Meeting
            (
                meetingDto.Name,
                meetingDto.Description,
                meetingDto.StartDate,
                meetingDto.EndDate,
                meetingDto.ImageUrl,
                meetingDto.MaxUsers,
                meetingDto.MinAge,
                (GenderType)meetingDto.Gender!,
                user,
                meetingDto.ShortDescription
            ));
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}