using EventService.Presentation.Grpc;
using HttpGateway.Clients.Abstractions;
using HttpGateway.Clients.GrpcClients;
using HttpGateway.Clients.GrpcClients.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TicketService.Grpc.Promocodes;
using TicketService.Grpc.Tickets;

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

        services.Configure<VenueManagementClientGrpcOptions>(configuration.GetSection("GrpcClients:VenueManagement"));

        services.AddGrpcClient<VenueGrpcService.VenueGrpcServiceClient>((sp, o) =>
        {
            VenueManagementClientGrpcOptions options = sp.GetRequiredService<IOptions<VenueManagementClientGrpcOptions>>().Value;
            o.Address = new Uri(options.Url);
        });

        services.AddScoped<IVenueManagementClientGrpc, VenueManagementClientGrpc>();

        services.Configure<TicketServiceClientOptions>(configuration.GetSection("GrpcClients:TicketService"));
        services.AddGrpcClient<TicketsService.TicketsServiceClient>((sp, o) =>
        {
            TicketServiceClientOptions options = sp.GetRequiredService<IOptions<TicketServiceClientOptions>>().Value;
            o.Address = new Uri(options.Url);
        });

        services.Configure<PromocodeServiceClientOptions>(configuration.GetSection("GrpcClients:PromocodesService"));
        services.AddGrpcClient<PromocodesService.PromocodesServiceClient>((sp, o) =>
        {
            PromocodeServiceClientOptions options = sp.GetRequiredService<IOptions<PromocodeServiceClientOptions>>().Value;
            o.Address = new Uri(options.Url);
        });
        services.AddScoped<IPromocodeServiceGrpcClient, PromocodeServiceGrpcClient>();
        services.AddScoped<ITicketServiceGrpcClient, TicketServiceGrpcClient>();
        return services;
    }
}