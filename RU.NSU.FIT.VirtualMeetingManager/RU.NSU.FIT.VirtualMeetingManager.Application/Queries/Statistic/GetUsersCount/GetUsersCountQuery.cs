using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Statistic.GetUsersCount;

public record GetUsersCountQuery : IRequest<int>
{
    [UsedImplicitly]
    public sealed class GetUsersListQueryHandler : IRequestHandler<GetUsersCountQuery, int>
    {
        private readonly IVMMDbContext _vmmDbContext;

        public GetUsersListQueryHandler(IVMMDbContext vmmDbContext)
        {
            _vmmDbContext = vmmDbContext;
        }

        public async Task<int> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
        {
            var query = _vmmDbContext.Users;

            var totalCount = await query.CountAsync(cancellationToken);
            
            return totalCount;
        }
    }
}