using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain;
using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.Authorization;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.Authorization.Models;

namespace RU.NSU.FIT.VirtualMeetingManager.Authentication.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private readonly IVMMDbContext _kcDbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AuthService(ITokenService tokenService, UserManager<User> userManager, IVMMDbContext kcDbContext, IDateTimeProvider dateTimeProvider)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _kcDbContext = kcDbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<AuthResponse> SignInAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(email);

        // Проверяем, что пользователь существует, а пароль корректный
        if (user is null || await _userManager.CheckPasswordAsync(user, password) is false)
        {
            throw new BadRequestException("Неверный логин или пароль");
        }
        
        // Генерируем новые токены
        var accessToken = await _tokenService.CreateAccessTokenAsync(user);
        var refreshToken = _tokenService.CreateRefreshToken(user);
        await _userManager.UpdateAsync(user);
        
        // Возвращаем пару access и refresh токенов
        return new AuthResponse(accessToken, refreshToken.Id.ToString());
    }

    public async Task<AuthResponse> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken)
    {
        var now = _dateTimeProvider.UtcNow;
        
        var user = _tokenService.GetUserFromAccessToken(accessToken);

        var existingRefreshToken = await _kcDbContext.RefreshTokens
            .FirstOrDefaultAsync(t => t.Id == Guid.Parse(refreshToken), cancellationToken);
        if (existingRefreshToken is null)
        {
            throw new BadRequestException("Invalid access or refresh token.");
        }

        if (existingRefreshToken.ValidUntil <= now)
        {
            _kcDbContext.RefreshTokens.Remove(existingRefreshToken);
            await _kcDbContext.SaveChangesAsync(cancellationToken);
            throw new BadRequestException("Invalid access or refresh token.");
        }

        var newAccessToken = await _tokenService.CreateAccessTokenAsync(user);
        var newRefreshToken = _tokenService.CreateRefreshToken(user);

        _kcDbContext.RefreshTokens.Remove(existingRefreshToken);
        await _kcDbContext.SaveChangesAsync(cancellationToken);
        
        return new AuthResponse(newAccessToken, newRefreshToken.Id.ToString());
    }
}