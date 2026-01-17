using EventService.Presentation.Grpc;

namespace HttpGateway.Clients.Abstractions;

public interface ISeatValidationClientGrpc
{
    Task<SeatExistsResponse> SeatExistsAsync(SeatExistsRequest request, CancellationToken ct);

    Task<SeatAvailableResponse> IsSeatAvailableAsync(IsSeatAvailableRequest request, CancellationToken ct);

    Task<SeatStatusResponse> GetSeatStatusAsync(GetSeatStatusRequest request, CancellationToken ct);

    Task<BookSeatsResponse> BookSeatsAsync(BookSeatsRequest request, CancellationToken ct);

    Task<ReturnSeatsResponse> ReturnSeatsAsync(ReturnSeatsRequest request, CancellationToken ct);
}