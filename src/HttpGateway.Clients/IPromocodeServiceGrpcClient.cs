using TicketService.Grpc.Promocodes;

namespace HttpGateway.Clients;

public interface IPromocodeServiceGrpcClient
{
    Task CreatePromocodeAsync(string promo, long discountPercentage, long count, CancellationToken cancellationToken);

    Task<Promocode> GetPromocodeAsync(string code, CancellationToken cancellationToken);
}