using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.EmailService;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.ResetPassword;

public record ResetPasswordCommand : IRequest
{
    public Guid UserId { get; init; }
    public string ResetToken { get; init; } = string.Empty;
    public string NewPassword { get; init; } = string.Empty;
    
    [UsedImplicitly]
    public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public ResetPasswordCommandHandler(UserManager<User> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user is null)
            {
                return;
            }

            var result = await _userManager.ResetPasswordAsync(user, request.ResetToken, request.NewPassword);

            if (result.Succeeded is false)
            {
                throw new BadRequestException($"Ошибки при сбросе пароля: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            await _emailService.SendPasswordHasBeenResetEmailAsync(user.Email, cancellationToken);
        }
    }
}