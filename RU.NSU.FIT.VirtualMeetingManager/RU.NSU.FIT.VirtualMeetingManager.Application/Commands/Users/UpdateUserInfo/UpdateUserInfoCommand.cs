using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Base.EditUserCommand;
using RU.NSU.FIT.VirtualMeetingManager.Application.Extensions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Queries.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Users.UpdateUserInfo;

public class UpdateUserInfoCommand : IRequest, IEditUserCommand
{
    public Guid UserId { get; init; }

    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateOnly BirthDate { get; init; }
    public GenderType Gender { get; init; }
    public string? Phone { get; init; }

    [UsedImplicitly]
    public class UpdateUserInfoCommandHandler : IRequestHandler<UpdateUserInfoCommand>
    {
        private readonly IVMMDbContext _dbContext;

        private readonly ICurrentUser _currentUser;

        private readonly EditUserCommandValidator _commandValidator;

        public UpdateUserInfoCommandHandler(
            IVMMDbContext dbContext,
            ICurrentUser currentUser, 
            EditUserCommandValidator commandValidator)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
            _commandValidator = commandValidator;
        }

        public async Task Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            EntityNotFoundException.ThrowIfNull(user, "В системе отсутствует пользователь с Id={0}", request.UserId);

            if (user.Id != _currentUser.Id && _currentUser.CanEditAllUsersDetails() is false)
            {
                throw new ForbiddenException("Текущий пользователь не имеет прав на изменение данных данного пользователя");
            }
            
            // Проверяем команду
            _commandValidator.ValidateAndThrow(request);
            
            user.UpdateUserInfo(
                request.FirstName,
                request.LastName,
                request.BirthDate,
                (VirtualManager.Domain.ValueTypes.GenderType) request.Gender,
                request.Phone);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}