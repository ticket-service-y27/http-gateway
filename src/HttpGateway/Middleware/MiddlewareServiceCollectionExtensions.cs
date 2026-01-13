namespace HttpGateway.Middleware;

public static class MiddlewareServiceCollectionExtensions
{
    public static IServiceCollection AddMiddleware(this IServiceCollection services)
    {
        services.AddScoped<ExceptionMiddleware>();
        return services;
    }

    public static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}