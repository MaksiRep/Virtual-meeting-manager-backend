using RU.NSU.FIT.VirtualMeetingManager.Application.Services.Authorization.Models;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Services.Authorization;

public interface IAuthService
{
    public Task<AuthResponse> SignInAsync(string email, string password, CancellationToken cancellationToken = default);
    public Task<AuthResponse> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default);
}