using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RU.NSU.FIT.VirtualManager.Domain;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualManager.Domain.Exceptions;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.Authorization;
using RU.NSU.FIT.VirtualMeetingManager.Application.Settings;

namespace RU.NSU.FIT.VirtualMeetingManager.Authentication.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly AuthSettings _authSettings;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public TokenService(UserManager<User> userManager, AuthSettings authSettings, IDateTimeProvider dateTimeProvider)
    {
        _userManager = userManager;
        _dateTimeProvider = dateTimeProvider;
        _authSettings = authSettings;
    }
    
    public async Task<string> CreateAccessTokenAsync(User user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var now = _dateTimeProvider.UtcNow;
        var claims = new List<Claim>
        {
            new(InnerClaimTypes.UserId, user.Id.ToString()),
            new(InnerClaimTypes.UserName, user.UserName!),
            new(InnerClaimTypes.IssuedAt, now.ToString(CultureInfo.InvariantCulture)),
        };

        var rolesClaims = userRoles.Select(r => new Claim(InnerClaimTypes.UserRole, r));
        claims.AddRange(rolesClaims);

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.SecretKey!));

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: now.Add(TimeSpan.FromMinutes(_authSettings.AccessTokenLifetimeMinutes)),
            notBefore: now,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public RefreshToken CreateRefreshToken(User user)
    {
        var now = _dateTimeProvider.UtcNow;
        var refreshToken = new RefreshToken(user, now, now.AddDays(_authSettings.RefreshTokenLifetimeDays));
        user.AddRefreshToken(refreshToken);
        return refreshToken;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.SecretKey!));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }


        return principal;
    }

    public User GetUserFromAccessToken(string accessToken)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.SecretKey!));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == InnerClaimTypes.UserId);

        if (userIdClaim is null)
        {
            throw new UnauthorizedException("User Id claim is not set");
        }
        
        var user = _userManager.Users
            .FirstOrDefault(u => u.Id == Guid.Parse(userIdClaim.Value));

        if (user is null)
        {
            throw new UnauthorizedException("User is not found");
        }

        return user;
    }
}