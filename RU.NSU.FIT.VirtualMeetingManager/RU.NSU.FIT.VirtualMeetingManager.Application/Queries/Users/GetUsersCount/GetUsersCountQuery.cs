namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Users.GetUsersCount;

using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;


public record GetUsersCountQuery : IRequest<UsersCountDto>
{
    [UsedImplicitly]
    public sealed class GetUsersListQueryHandler : IRequestHandler<GetUsersCountQuery, UsersCountDto>
    {
        private readonly IVMMDbContext _vmmDbContext;

        public GetUsersListQueryHandler(IVMMDbContext vmmDbContext)
        {
            _vmmDbContext = vmmDbContext;
        }

        public async Task<UsersCountDto> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
        {
            var query = _vmmDbContext.Users;

            var totalCount = await query.CountAsync(cancellationToken);
            
            return new UsersCountDto()
            {
                TotalCount = totalCount
            };
        }
    }
}