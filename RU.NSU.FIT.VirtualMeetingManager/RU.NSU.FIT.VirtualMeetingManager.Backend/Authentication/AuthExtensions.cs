using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RU.NSU.FIT.VirtualManager.Domain.Auth;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Authentication;

public static class AuthExtensions
{
    public static IServiceCollection AddAuthenticationAndAuthorization(
        this IServiceCollection services, SymmetricSecurityKey signingKey)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    NameClaimType = InnerClaimTypes.UserName,
                    RoleClaimType = InnerClaimTypes.UserRole,
                    ValidateLifetime = true
                };
                
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Is-Token-Expired", "true");
                        }
                
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
}