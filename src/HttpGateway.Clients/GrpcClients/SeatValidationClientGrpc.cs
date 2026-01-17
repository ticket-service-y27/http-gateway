using EventService.Presentation.Grpc;
using HttpGateway.Clients.Abstractions;

namespace HttpGateway.Clients.GrpcClients;

public sealed class SeatValidationClientGrpc : ISeatValidationClientGrpc
{
    private readonly SeatValidationGrpcService.SeatValidationGrpcServiceClient _client;

    public SeatValidationClientGrpc(SeatValidationGrpcService.SeatValidationGrpcServiceClient client)
    {
        _client = client;
    }

    public async Task<SeatExistsResponse> SeatExistsAsync(SeatExistsRequest request, CancellationToken ct)
    {
        return await _client.SeatExistsAsync(request, cancellationToken: ct);
    }

    public async Task<SeatAvailableResponse> IsSeatAvailableAsync(IsSeatAvailableRequest request, CancellationToken ct)
    {
        return await _client.IsSeatAvailableAsync(request, cancellationToken: ct);
    }

    public async Task<SeatStatusResponse> GetSeatStatusAsync(GetSeatStatusRequest request, CancellationToken ct)
    {
        return await _client.GetSeatStatusAsync(request, cancellationToken: ct);
    }

    public async Task<BookSeatsResponse> BookSeatsAsync(BookSeatsRequest request, CancellationToken ct)
    {
        return await _client.BookSeatsAsync(request, cancellationToken: ct);
    }

    public async Task<ReturnSeatsResponse> ReturnSeatsAsync(ReturnSeatsRequest request, CancellationToken ct)
    {
        return await _client.ReturnSeatsAsync(request, cancellationToken: ct);
    }
}