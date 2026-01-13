using HttpGateway.Clients.GrpcClients;
using HttpGateway.Clients.GrpcClients.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HttpGateway.Clients;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcClients(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<UserGrpcClientOptions>(configuration.GetSection("GrpcClients:UserService"));

        services.AddGrpcClient<Users.UserService.Contracts.UserService.UserServiceClient>((sp, o) =>
        {
            UserGrpcClientOptions options = sp.GetRequiredService<IOptions<UserGrpcClientOptions>>().Value;
            o.Address = new Uri(options.Url);
        });
        services.AddScoped<IUserGrpcClient, UserGrpcClient>();

        return services;
    }
}