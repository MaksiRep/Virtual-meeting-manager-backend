using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Meetings.GetMeetingUsers;

public record GetMeetingUsersQuery : IRequest<ICollection<UserDto>>
{
    public int MeetingId { get; init; }
    
    [UsedImplicitly]
    public sealed class GetMeetingUsersQueryHandler : IRequestHandler<GetMeetingUsersQuery, ICollection<UserDto>>
    {
        private readonly IVMMDbContext _vmmDbContext;

        public GetMeetingUsersQueryHandler(IVMMDbContext vmmDbContext)
        {
            _vmmDbContext = vmmDbContext;
        }
        
        public async Task<ICollection<UserDto>> Handle(GetMeetingUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _vmmDbContext.Meetings
                .FilterById(request.MeetingId)
                .SelectMany(m => m.Users)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    Gender = (GenderType) u.Gender,
                })
                .ToListAsync(cancellationToken);
            
            return users;
            
        }
    }
}