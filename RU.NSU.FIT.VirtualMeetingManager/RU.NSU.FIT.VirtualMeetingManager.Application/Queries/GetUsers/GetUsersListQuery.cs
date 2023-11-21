using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.GetUsers;

public record GetUsersListQuery : IRequest<GetUserListResponse>
{
    public string? Email { get; init; }
    
    public int Skip { get; init; }
    
    public int Take { get; init; }
    
    public sealed class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, GetUserListResponse>
    {
        private readonly IVMMDbContext _vmmDbContext;

        public GetUsersListQueryHandler(IVMMDbContext vmmDbContext)
        {
            _vmmDbContext = vmmDbContext;
        }

        public async Task<GetUserListResponse> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var query = _vmmDbContext.Users
                .AsQueryable();
            var totalCount = await query.CountAsync(cancellationToken);
            
            var users = await query
                .Where(u => request.Email.IsNullOrEmpty() || u.Email == request.Email)
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    Gender = u.Gender
                })
                .ToListAsync(cancellationToken);

            return new GetUserListResponse()
            {
                Items = users,
                TotalCount = totalCount
            } ;
        }
    }
}