using EventService.Presentation.Grpc;
using HttpGateway.Clients.Abstractions;
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

        services.Configure<EventManagerClientGrpcOptions>(configuration.GetSection("GrpcClients:EventManagerService"));

        services.AddGrpcClient<EventManagerGrpcService.EventManagerGrpcServiceClient>((sp, o) =>
        {
            EventManagerClientGrpcOptions options = sp.GetRequiredService<IOptions<EventManagerClientGrpcOptions>>().Value;
            o.Address = new Uri(options.Url);
        });
        services.AddScoped<IEventManagerClientGrpc, EventManagerClientGrpc>();

        services.Configure<SeatValidationClientGrpcOptions>(configuration.GetSection("GrpcClients:SeatValidator"));

        services.AddGrpcClient<SeatValidationGrpcService.SeatValidationGrpcServiceClient>((sp, o) =>
        {
            SeatValidationClientGrpcOptions options = sp.GetRequiredService<IOptions<SeatValidationClientGrpcOptions>>().Value;
            o.Address = new Uri(options.Url);
        });
        services.AddScoped<ISeatValidationClientGrpc, SeatValidationClientGrpc>();

        return services;
    }
}