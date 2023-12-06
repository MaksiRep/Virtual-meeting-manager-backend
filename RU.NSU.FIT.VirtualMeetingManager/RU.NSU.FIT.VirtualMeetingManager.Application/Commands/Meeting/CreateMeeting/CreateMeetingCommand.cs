using MediatR;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;
using GenderType = RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base.GenderType;
using GenderTypeDto = RU.NSU.FIT.VirtualManager.Domain.ValueTypes.GenderType;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meeting.CreateMeeting;

public class CreateMeetingCommand : IRequest
{
    public string Name { get; init; }

    public string Description { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public string ImageUrl { get; init; }

    public int? MaxUsers { get; init; }

    public int? MinAge { get; init; }

    public GenderType? Gender { get; init; }

    public string ShortDescription { get; init; }

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
            var user = await _dbContext.Users.FindAsync(_currentUser.Id);
            _dbContext.Meetings.Add(new global::Meeting
            (
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate,
                request.ImageUrl,
                request.MaxUsers,
                request.MinAge,
                (GenderTypeDto)request.Gender!,
                user,
                request.ShortDescription
            ));
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}