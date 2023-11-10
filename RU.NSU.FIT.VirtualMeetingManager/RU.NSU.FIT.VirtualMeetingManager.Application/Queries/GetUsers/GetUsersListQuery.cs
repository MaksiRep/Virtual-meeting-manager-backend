using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.GetUsers;

public record GetUsersListQuery : IRequest<ICollection<UserDto>>
{
    public string? Email { get; init; }
    
    public sealed class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, ICollection<UserDto>>
    {
        private readonly IVMMDbContext _vmmDbContext;

        public GetUsersListQueryHandler(IVMMDbContext vmmDbContext)
        {
            _vmmDbContext = vmmDbContext;
        }

        public async Task<ICollection<UserDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            // TODO: доработать метод получения списка юзеров
            var users = await _vmmDbContext.Users
                .Where(u => request.Email == null || u.Email == request.Email)
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