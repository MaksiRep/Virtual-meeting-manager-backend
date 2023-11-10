using JetBrains.Annotations;
using MediatR;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.Authorization;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Auth.RefreshToken;

public record RefreshTokenCommand : IRequest<AuthResponse>
{
    public string AccessToken { get; init; } = null!;
    public string RefreshToken { get; init; } = null!;

    [UsedImplicitly]
    public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var response = await _authService.RefreshTokenAsync(request.AccessToken, request.RefreshToken, cancellationToken);

            return new AuthResponse(response.AccessToken, response.RefreshToken);
        }
    }
}