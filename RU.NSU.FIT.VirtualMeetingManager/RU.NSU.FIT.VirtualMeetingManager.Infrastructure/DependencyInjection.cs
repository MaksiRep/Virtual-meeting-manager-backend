using DI.KC.Portal.Infrastructure.Email;
using Microsoft.Extensions.DependencyInjection;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.Authorization;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services.EmailService;
using RU.NSU.FIT.VirtualMeetingManager.Application.Settings;
using RU.NSU.FIT.VirtualMeetingManager.Authentication.Services;
using RU.NSU.FIT.VirtualMeetingManager.Email;

namespace RU.NSU.FIT.VirtualMeetingManager;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services, AuthSettings? authSettings)
    {
        if (authSettings is null)
        {
            throw new ArgumentNullException(nameof(authSettings), "Не заданы настройки аутентификации");
        }

        services.AddSingleton(authSettings);
        
        return services
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IAuthService, AuthService>();
    }
    
    public static IServiceCollection AddEmailService(this IServiceCollection services, EmailServiceSettings? emailServiceSettings)
    {
        if (emailServiceSettings is null)
        {
            throw new ArgumentNullException(nameof(emailServiceSettings), "Не заданы настройки сервиса Email-рассылок");
        }
        
        services.AddSingleton(emailServiceSettings);
        
        return services
            .AddTransient<IEmailService, EmailService>();
    }
}