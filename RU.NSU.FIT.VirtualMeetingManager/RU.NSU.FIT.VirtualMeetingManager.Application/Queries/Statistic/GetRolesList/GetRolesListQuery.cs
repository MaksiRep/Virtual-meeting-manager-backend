using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Statistic.GetRolesList;

public record GetRoleListQuery : IRequest<ICollection<RoleDto>>
{
    [UsedImplicitly]
    public sealed class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, ICollection<RoleDto>>
    {
        private readonly IVMMDbContext _vmmDbContext;

        public GetRoleListQueryHandler(IVMMDbContext vmmDbContext)
        {
            _vmmDbContext = vmmDbContext;
        }

        public async Task<ICollection<RoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var result = await _vmmDbContext.Roles
                .Select(role => new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name!,
                    Type = role.Type.ToOuterType(),
                })
                .ToListAsync(cancellationToken);
            
            return result;
        }
    }
}