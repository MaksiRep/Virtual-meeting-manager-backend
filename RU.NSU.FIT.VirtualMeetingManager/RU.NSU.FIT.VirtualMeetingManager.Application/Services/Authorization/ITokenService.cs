using System.Security.Claims;
using RU.NSU.FIT.VirtualManager.Domain.Entities;

namespace RU.NSU.FIT.VirtualMeetingManager.Application.Services.Authorization;

public interface ITokenService
{
    public Task<string> CreateAccessTokenAsync(User user);
    public RefreshToken CreateRefreshToken(User user);
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string accessToken);
    public User GetUserFromAccessToken(string accessToken);
}