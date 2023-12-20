using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;
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

            var user = await _dbContext.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, "В системе отсутствует пользователь с Id={0}", request.UserId);

            var roles = await _dbContext.Roles
                .Where(r => request.RolesIds.Contains(r.Id))
                .ToListAsync(cancellationToken);

            var notFoundRolesIds = request.RolesIds
                .Except(roles.Select(r => r.Id))
                .ToList();

            EntityNotFoundException.ThrowIfAny(notFoundRolesIds, "В системе отсутствуют роли с Id={0}",
                string.Join(", ", notFoundRolesIds.Select(id => id.ToString())));

            if (user.Id == _currentUser.Id && roles.Exists(r => r.Type == RoleType.MainAdmin) is false)
            {
                throw new BadRequestException(
                    "Текущий пользователь не может снять с себя права Главного Администратора");
            }

            user.UpdateUserRoles(roles);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}