using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.GetUsers;

public record GetUsersListByMeetingQuery : IRequest<ICollection<UserDto>>
{
    public int MeetingId { get; init; }
    
    public sealed class GetUsersListByMeetingQueryHandler : IRequestHandler<GetUsersListByMeetingQuery, ICollection<UserDto>>
    {
        private readonly IVMMDbContext _vmmDbContext;

        public GetUsersListByMeetingQueryHandler(IVMMDbContext vmmDbContext)
        {
            _vmmDbContext = vmmDbContext;
        }
        
        public async Task<ICollection<UserDto>> Handle(GetUsersListByMeetingQuery request, CancellationToken cancellationToken)
        {
            var users = await _vmmDbContext.Meetings
                .Where(m => m.Id == request.MeetingId)
                .SelectMany(m => m.Users)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                })
                .ToListAsync(cancellationToken);
            
            return users;
            
        }
    }
}