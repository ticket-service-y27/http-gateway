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

        services.Configure<EventServiceClientGrpcOptions>(configuration.GetSection("GrpcClients:EventService"));

        services.AddGrpcClient<EventManagerGrpcService.EventManagerGrpcServiceClient>((sp, o) =>
        {
            EventServiceClientGrpcOptions options = sp.GetRequiredService<IOptions<EventServiceClientGrpcOptions>>().Value;
            o.Address = new Uri(options.Url);
        });
        services.AddScoped<IEventManagerClientGrpc, EventManagerClientGrpc>();

        services.AddGrpcClient<SeatValidationGrpcService.SeatValidationGrpcServiceClient>((sp, o) =>
        {
            EventServiceClientGrpcOptions options = sp.GetRequiredService<IOptions<EventServiceClientGrpcOptions>>().Value;
            o.Address = new Uri(options.Url);
        });
        services.AddScoped<ISeatValidationClientGrpc, SeatValidationClientGrpc>();

        services.AddGrpcClient<VenueGrpcService.VenueGrpcServiceClient>((sp, o) =>
        {
            EventServiceClientGrpcOptions options = sp.GetRequiredService<IOptions<EventServiceClientGrpcOptions>>().Value;
            o.Address = new Uri(options.Url);
        });
        services.AddScoped<IVenueManagementClientGrpc, VenueManagementClientGrpc>();

        services.Configure<PaymentGrpcClientOptions>(configuration.GetSection("GrpcClients:PaymentService"));
        services.AddGrpcClient<PaymentService.Grpc.Wallets.WalletService.WalletServiceClient>((sp, o) =>
        {
            PaymentGrpcClientOptions options = sp.GetRequiredService<IOptions<PaymentGrpcClientOptions>>().Value;
            o.Address = new Uri(options.Url);
        });
        services.AddScoped<IWalletGrpcClient, WalletGrpcClient>();

        services.AddGrpcClient<PaymentService.Grpc.WalletTransactions.WalletTransactionsService.WalletTransactionsServiceClient>((sp, o) =>
        {
            PaymentGrpcClientOptions options = sp.GetRequiredService<IOptions<PaymentGrpcClientOptions>>().Value;
            o.Address = new Uri(options.Url);
        });
        services.AddScoped<ITransactionGrpcClient, TransactionGrpcClient>();

        return services;
    }
}