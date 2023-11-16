using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.GetUsers;

public record GetUsersListByEventQuery : IRequest<ICollection<UserDto>>
{
    public int MeetingId { get; init; }
    
    public sealed class GetUsersListByEventQueryHandler : IRequestHandler<GetUsersListByEventQuery, ICollection<UserDto>>
    {
        private readonly IVMMDbContext _vmmDbContext;

        public GetUsersListByEventQueryHandler(IVMMDbContext vmmDbContext)
        {
            _vmmDbContext = vmmDbContext;
        }
        
        public async Task<ICollection<UserDto>> Handle(GetUsersListByEventQuery request, CancellationToken cancellationToken)
        {
            var meeting = _vmmDbContext.Meetings
                .Find(request.MeetingId);
            
            var users = await _vmmDbContext.Users
                .Where(u => meeting != null && u.Meetings.Contains(meeting))
                .Select(u => new UserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                })
                .ToListAsync(cancellationToken);
            
            return users;
        }
    }
}