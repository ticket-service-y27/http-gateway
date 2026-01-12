using HttpGateway.Controllers.GrpcClients;
using HttpGateway.Controllers.GrpcClients.Options;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;

namespace HttpGateway;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationGrpcClients(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<UserGrpcClientOptions>(configuration.GetSection("Presentation:Grpc:UserServiceClient"));

        services.AddGrpcClient<UserService.Users.Contracts.UserService.UserServiceClient>((sp, o) =>
        {
            UserGrpcClientOptions options = sp.GetRequiredService<IOptions<UserGrpcClientOptions>>().Value;
            o.Address = new Uri(options.Url);
        });

        services.AddScoped<IUserGrpcClient, UserGrpcClient>();

        return services;
    }

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