using EventService.Presentation.Grpc;
using HttpGateway.Clients.Abstractions;
using System.Runtime.CompilerServices;

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

    public async IAsyncEnumerable<EventResponse> GetAllEventsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        using Grpc.Core.AsyncServerStreamingCall<EventResponse> call =
            _client.GetAllEvents(new GetAllEventsRequest(), cancellationToken: ct);

        while (await call.ResponseStream.MoveNext(ct))
        {
            yield return call.ResponseStream.Current;
        }
    }
}