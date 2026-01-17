using EventService.Presentation.Grpc;
using HttpGateway.Clients.Abstractions;

namespace HttpGateway.Clients.GrpcClients;

public sealed class EventManagerClientGrpc : IEventManagerClientGrpc
{
    private readonly EventManagerGrpcService.EventManagerGrpcServiceClient _client;

    public EventManagerClientGrpc(
        EventManagerGrpcService.EventManagerGrpcServiceClient client)
    {
        _client = client;
    }

    public async Task<EventResponse> CreateEventAsync(
        CreateEventRequest request,
        CancellationToken ct)
    {
        return await _client.CreateEventAsync(
            request,
            cancellationToken: ct);
    }

    public async Task<EventResponse> UpdateEventAsync(
        UpdateEventRequest request,
        CancellationToken ct)
    {
        return await _client.UpdateEventAsync(
            request,
            cancellationToken: ct);
    }
}