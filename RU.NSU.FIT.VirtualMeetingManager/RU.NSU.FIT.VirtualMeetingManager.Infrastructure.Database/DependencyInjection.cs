using DI.KC.Portal.Infrastructure.Database;
using DI.KC.Portal.Infrastructure.Database.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RU.NSU.FIT.VirtualMeetingManager.Application.Services;
using Serilog;

namespace RU.NSU.FIT.VirtualMeetingManager;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, DatabaseSettings? databaseSettings)
    {
        if (databaseSettings?.ConnectionString is null)
        {
            throw new ArgumentNullException(nameof(databaseSettings), "Не заданы настройки БД");
        }

        services.AddDbContext<VMMDbContext>(opt => opt.UseNpgsql(databaseSettings.ConnectionString));
        services.AddScoped<IVMMDbContext>(isp => isp.GetRequiredService<VMMDbContext>());
        services.AddTransient<IVMMDbMigrator, VMMDbMigrator>();

        return services;
    }

    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var dbMigrator = scope.ServiceProvider.GetRequiredService<IVMMDbMigrator>();
        try
        {
            dbMigrator.Migrate();
        }
        catch (Exception e)
        {
            Log.Error("Возникла ошибка при миграции БД");
            throw;
        }

        return host;
    }
}