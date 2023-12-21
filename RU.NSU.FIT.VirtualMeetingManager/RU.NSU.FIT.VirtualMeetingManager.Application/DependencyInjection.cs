using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RU.NSU.FIT.VirtualManager.Domain;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Base.EditUserCommand;
using RU.NSU.FIT.VirtualMeetingManager.Application.Commands.Meetings.Base;
using RU.NSU.FIT.VirtualMeetingManager.Application.Common;

namespace RU.NSU.FIT.VirtualMeetingManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddTransient<EditUserCommandValidator>()
            .AddTransient<EditMeetingCommandValidator>();
    }
}