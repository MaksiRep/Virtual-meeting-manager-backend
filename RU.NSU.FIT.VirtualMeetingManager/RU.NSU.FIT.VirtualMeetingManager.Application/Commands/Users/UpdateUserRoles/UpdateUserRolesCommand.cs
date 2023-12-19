using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Users.UpdateUserRoles;

public record UpdateUserRolesCommand : IRequest
{
    public Guid UserId { get; init; }

    public IList<Guid> RolesIds { get; init; }

    private static Guid Int2Guid(int value)
    {
        byte[] bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }
    
    [UsedImplicitly]
    public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;


        public UpdateUserRolesCommandHandler(IVMMDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            foreach (var ri in request.RolesIds)
            {
                var role = await _dbContext.Roles
                    .FirstOrDefaultAsync(r => ri == r.Id, cancellationToken);
                EntityNotFoundException.ThrowIfNull(role, "В системе отсутствует роль с Id={0}", ri);
            }
            
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, "В системе отсутствует пользователь с Id={0}", request.UserId);

            if ((request.RolesIds.Contains(Int2Guid(3)) && _currentUser.Roles.Contains(RoleType.MainAdmin) is false)
                || (request.RolesIds.Contains(Int2Guid(3)) is false
                    && user.Id == _currentUser.Id
                    && _currentUser.Roles.Contains(RoleType.MainAdmin)))
            {
                throw new ForbiddenException(
                    "Текущий пользователь не имеет прав на изменение ролей данного пользователя");
            }

            var roles = await _dbContext.Roles
                .Where(r => request.RolesIds.Contains(r.Id))
                .ToListAsync(cancellationToken);
            
            user.UpdateUserRoles(roles);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}