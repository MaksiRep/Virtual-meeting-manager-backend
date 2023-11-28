using MediatR;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.CreateMeeting;

public class CreateMeetingQuery : IRequest
{
    public MeetingDto MeetingDto { get; init; }

    public class GetMeetingsListQueryHandler : IRequestHandler<CreateMeetingQuery>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;
        
        public GetMeetingsListQueryHandler(IVMMDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task Handle(CreateMeetingQuery request, CancellationToken cancellationToken)
        {
            var meetingDto = request.MeetingDto;
            var user = await _dbContext.Users.FindAsync(_currentUser.Id);
            _dbContext.Meetings.Add(new Meeting
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