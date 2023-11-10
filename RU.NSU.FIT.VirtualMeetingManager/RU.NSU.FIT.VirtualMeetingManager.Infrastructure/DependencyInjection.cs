using Microsoft.Extensions.DependencyInjection;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.Authorization;
using RU.NSU.FIT.VirtualMeetingManager.Authentication.Services;

namespace RU.NSU.FIT.VirtualMeetingManager;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IAuthService, AuthService>();
    }
}