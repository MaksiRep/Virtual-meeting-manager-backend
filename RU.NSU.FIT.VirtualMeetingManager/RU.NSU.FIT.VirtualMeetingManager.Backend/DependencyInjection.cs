using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualMeetingManager.Application;
using RU.NSU.FIT.VirtualMeetingManager.Application.Settings;
using RU.NSU.FIT.VirtualMeetingManager.Backend.Authentication;
using RU.NSU.FIT.VirtualMeetingManager.Backend.Middlewares;
using RU.NSU.FIT.VirtualMeetingManager.Backend.Middlewares.Logging;
using RU.NSU.FIT.VirtualMeetingManager.Backend.Settings;
using RU.NSU.FIT.VirtualMeetingManager.Backend.Swagger;
using Serilog;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Settings
        var webAppSettings = builder.Configuration.Get<WebAppSettings>()
                             ?? throw new NullReferenceException("Не заданы настройки приложения");
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            webAppSettings.Auth?.SecretKey ?? throw new NullReferenceException("Ключ подписи токенов не задан.")));

        builder.Services
            .AddSingleton(webAppSettings)
            .AddSingleton(webAppSettings
                .Frontend ?? throw new NullReferenceException("Не заданы настройки Frontend-приложения"));
        
        
        // Logging
        builder.Host.UseSerilog((context, cfg) => { cfg.ReadFrom.Configuration(context.Configuration); });

        // Services
        builder.Services
            .AddApplication()
            .AddAuthServices(webAppSettings.Auth)
            .AddEmailService(webAppSettings.EmailService)
            .AddDatabase(webAppSettings.Database)
            ;

        builder.Services
            .AddMvc()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            });

        // Authentication
        builder.Services
            .AddAuthenticationAndAuthorization(signingKey);

        // Identity
        builder.Services
            .AddIdentityCore<User>(s => s.Password.RequireNonAlphanumeric = false)
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddEntityFrameworkStores<VMMDbContext>()
            .AddDefaultTokenProviders();

        // Swagger
        builder.Services
            .AddSwagger();
        
        // Cors
        builder.Services.AddCors();
        
        return builder;
    }
    
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        // Localization
        app.UseRequestLocalization(opt =>
        {
            // configure localization
        });

        // Https
        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }
        

        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("v1/swagger.json", "Virtual Meeting Manager"); });

        app.UseRouting();
        
        // Logging
        app.UseMiddleware<RequestResponseLoggingMiddleware>();
        
        // Cors
        if (!app.Environment.IsProduction())
        {
            app.UseCors(cfg => cfg
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
        }

        // Exception handling
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseMiddleware<ExceptionLoggingMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}