using EventService.Presentation.Grpc;
using Google.Protobuf.WellKnownTypes;
using HttpGateway.Clients.Abstractions;

namespace HttpGateway.Clients.GrpcClients;

public sealed class VenueManagementClientGrpc : IVenueManagementClientGrpc
{
    private readonly VenueGrpcService.VenueGrpcServiceClient _client;

    public VenueManagementClientGrpc(VenueGrpcService.VenueGrpcServiceClient client)
    {
        _client = client;
    }

    public async Task<VenueResponse> CreateVenueAsync(CreateVenueRequest request, CancellationToken ct)
    {
        return await _client.CreateVenueAsync(request, cancellationToken: ct);
    }

    public async Task<VenueResponse> UpdateVenueAsync(UpdateVenueRequest request, CancellationToken ct)
    {
        return await _client.UpdateVenueAsync(request, cancellationToken: ct);
    }

    public async Task<HallSchemeResponse> AddHallSchemeAsync(AddHallSchemeRequest request, CancellationToken ct)
    {
        return await _client.AddHallSchemeAsync(request, cancellationToken: ct);
    }

    public async Task<Empty> RemoveHallSchemeAsync(RemoveHallSchemeRequest request, CancellationToken ct)
    {
        return await _client.RemoveHallSchemeAsync(request, cancellationToken: ct);
    }

    public async Task<HallSchemeResponse> GetHallSchemeByIdAsync(GetHallSchemeByIdRequest request, CancellationToken ct)
    {
        return await _client.GetHallSchemeByIdAsync(request, cancellationToken: ct);
    }

    public async Task<HallSchemesList> GetVenueHallSchemesAsync(GetVenueHallSchemesRequest request, CancellationToken ct)
    {
        return await _client.GetVenueHallSchemesAsync(request, cancellationToken: ct);
    }

    public async Task<VenueHasAvailableSchemeResponse> VenueHasAvailableSchemeAsync(VenueHasAvailableSchemeRequest request, CancellationToken ct)
    {
        return await _client.VenueHasAvailableSchemeAsync(request, cancellationToken: ct);
    }
}