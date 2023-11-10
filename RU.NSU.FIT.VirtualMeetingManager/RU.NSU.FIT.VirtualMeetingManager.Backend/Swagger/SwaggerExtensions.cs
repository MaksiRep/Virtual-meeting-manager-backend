using Microsoft.OpenApi.Models;

namespace RU.NSU.FIT.VirtualMeetingManager.Backend.Swagger;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services
            .AddSwaggerGen(opt =>
            {
                opt.SupportNonNullableReferenceTypes();
                opt.UseOneOfForPolymorphism();
                opt.UseDateOnlyTimeOnlyStringConverters();
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Specify the authorization token.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            Type = SecuritySchemeType.Http,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
    }
}