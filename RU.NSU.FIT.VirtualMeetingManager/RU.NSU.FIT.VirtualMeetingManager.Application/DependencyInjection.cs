using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RU.NSU.FIT.VirtualManager.Domain;
using RU.NSU.FIT.VirtualMeetingManager.Application.Common;

namespace RU.NSU.FIT.VirtualMeetingManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}