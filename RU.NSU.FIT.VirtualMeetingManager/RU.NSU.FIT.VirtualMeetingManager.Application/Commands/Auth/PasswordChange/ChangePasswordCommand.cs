using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.PasswordChange;

public class ChangePasswordCommand : IRequest
{
    public Guid UserId { get; init; }
    public string OldPassword { get; init; }
    public string NewPassword { get; init; }

    
    public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly UserManager<User> _userManager;
        
        private readonly ICurrentUser _currentUser;
        

        public ChangePasswordCommandHandler(UserManager<User> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.Id != request.UserId)
            {
                throw new ForbiddenException("Нет прав на изменение пароля пользователя");
            }
            
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                throw new BadRequestException("Такого пользователя не существует");
            }
            
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                throw new BadRequestException("Ошибка при изменении пароля");
            }
            
        }
    }
    
    
    
}