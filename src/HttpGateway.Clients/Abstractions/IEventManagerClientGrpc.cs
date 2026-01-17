using EventService.Presentation.Grpc;

namespace HttpGateway.Clients.Abstractions;

public interface IEventManagerClientGrpc
{
    Task<EventResponse> CreateEventAsync(CreateEventRequest request, CancellationToken ct);

    Task<EventResponse> UpdateEventAsync(UpdateEventRequest request, CancellationToken ct);

    IAsyncEnumerable<EventResponse> GetAllEventsAsync(CancellationToken ct);
}