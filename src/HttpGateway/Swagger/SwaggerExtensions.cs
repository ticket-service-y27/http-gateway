using Microsoft.OpenApi;

namespace HttpGateway.Swagger;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerSettings(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc(
                name: "v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Ticket Service Http Gateway",
                });

            o.AddSecurityDefinition(
                name: "Bearer",
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                });

            o.AddSecurityRequirement(doc =>
            {
                var requirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecuritySchemeReference("Bearer", doc),
                        new List<string>()
                    },
                };

                return requirement;
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerSettings(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(o =>
        {
            o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            o.RoutePrefix = string.Empty;
        });
        return app;
    }
}