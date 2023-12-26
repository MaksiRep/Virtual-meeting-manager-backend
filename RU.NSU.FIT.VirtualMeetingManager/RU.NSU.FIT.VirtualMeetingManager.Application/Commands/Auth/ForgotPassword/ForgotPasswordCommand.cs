using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.EmailService;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.ForgotPassword;

public record ForgotPasswordCommand : IRequest
{
    public string Email { get; init; }
    
    [UsedImplicitly]
    public sealed class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(UserManager<User> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user is null)
            {
                return;
            }
            
            var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _emailService.SendForgotPasswordEmailAsync(user.Email, passwordResetToken, user.Id, cancellationToken);
        }
    }
}