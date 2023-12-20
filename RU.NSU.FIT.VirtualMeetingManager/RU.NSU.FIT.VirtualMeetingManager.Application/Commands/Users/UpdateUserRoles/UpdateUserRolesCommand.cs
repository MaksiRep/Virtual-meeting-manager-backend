using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Users.UpdateUserRoles;

public record UpdateUserRolesCommand : IRequest
{
    public Guid UserId { get; init; }

    public IList<Guid> RolesIds { get; init; }

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
            if (_currentUser.CanEditUsersRoles() is false)
            {
                throw new ForbiddenException(
                    "Текущий пользователь не имеет прав на изменение ролей пользователей");
            }

            var dbRolesIds = await _dbContext.Roles
                .Select(r => r.Id)
                .ToListAsync(cancellationToken);

            var excDB = request.RolesIds
                .Except(dbRolesIds);

            EntityNotFoundException.ThrowIfAny(excDB, "В системе отсутствуют роли с Id={0}", excDB);

            var excRead = request.RolesIds
                .Except(dbRolesIds);

            EntityNotFoundException.ThrowIfAny(excRead,
                "При считывании списка новых ролей пользователя произошла ошибка", excRead);

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, "В системе отсутствует пользователь с Id={0}", request.UserId);

            var roles = await _dbContext.Roles
                .Where(r => request.RolesIds.Contains(r.Id))
                .ToListAsync(cancellationToken);

            if (roles.Exists(r => r.Name == RoleConstants.Admin) is false && user.Id == _currentUser.Id)
            {
                throw new ForbiddenException(
                    "Текущий пользователь не имеет прав на изменение ролей данного пользователя");
            }

            user.UpdateUserRoles(roles);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}