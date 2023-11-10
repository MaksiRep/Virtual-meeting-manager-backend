using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using MediatR;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.Authorization;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.SignIn;

public record SignInCommand : IRequest<AuthResponse>
{
    public string Email { get; init; } = string.Empty;

    [DataType(DataType.Password)] public string Password { get; init; } = string.Empty;

    [UsedImplicitly]
    public sealed class SignInCommandHandler : IRequestHandler<SignInCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public SignInCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var response = await _authService.SignInAsync(request.Email, request.Password, cancellationToken);

            return new AuthResponse(response.AccessToken, response.RefreshToken);
        }
    }
}