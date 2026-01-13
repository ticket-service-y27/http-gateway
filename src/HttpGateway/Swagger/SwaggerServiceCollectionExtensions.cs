using Microsoft.OpenApi;

namespace HttpGateway.Swagger;

public static class SwaggerServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerSettings(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(o => o.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Http Gateway",
            }));
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