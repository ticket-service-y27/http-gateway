using Grpc.Core;
using TicketService.Grpc.Promocodes;

namespace HttpGateway.Clients.GrpcClients;

public class PromocodeServiceGrpcClient : IPromocodeServiceGrpcClient
{
    private readonly PromocodesService.PromocodesServiceClient _promocodes;

    public PromocodeServiceGrpcClient(PromocodesService.PromocodesServiceClient promocodes)
    {
        _promocodes = promocodes;
    }

    public async Task CreatePromocodeAsync(string promo, long discountPercentage, long count, CancellationToken cancellationToken)
    {
        var req = new CreatePromocodeRequest
        {
            Promo = promo,
            DiscountPercentage = discountPercentage,
            Count = count,
        };

        await _promocodes.CreatePromocodeAsync(req, cancellationToken: cancellationToken);
    }

    public async Task<Promocode> GetPromocodeAsync(string code, CancellationToken cancellationToken)
    {
        var req = new GetPromocodeByCodeRequest { Code = code };
        GetPromocodeByCodeResponse resp = await _promocodes.GetPromocodeByCodeAsync(req, cancellationToken: cancellationToken);

        if (resp.Promocode is null)
            throw new RpcException(new Status(StatusCode.NotFound, "promocode not found"));

        return resp.Promocode;
    }
}