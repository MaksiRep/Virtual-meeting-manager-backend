using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualMeetingManager.Application.Common.Pagination;
using RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetUsersList;

public record GetUsersListQuery : IRequest<GetUserListResponse>, IPagedListRequest
{
    public string? Email { get; init; }

    public int Skip { get; init; }
    public int Take { get; init; }

    [UsedImplicitly]
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
                .FilterByEmail(request.Email);

            var totalCount = await query.CountAsync(cancellationToken);

            var users = await query
                .OrderByDefault()
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    Gender = (GenderType) u.Gender,
                    Roles = u.Roles.Select(r => r.Type.ToOuterType()).ToList()
                })
                .ToListAsync(cancellationToken);

            return new GetUserListResponse
            {
                Items = users,
                TotalCount = totalCount
            };
        }
    }
}