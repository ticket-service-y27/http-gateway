using EventService.Presentation.Grpc;
using Google.Protobuf.WellKnownTypes;

namespace HttpGateway.Clients.Abstractions;

public interface IVenueManagementClientGrpc
{
    Task<VenueResponse> CreateVenueAsync(CreateVenueRequest request, CancellationToken ct);

    Task<VenueResponse> UpdateVenueAsync(UpdateVenueRequest request, CancellationToken ct);

    Task<HallSchemeResponse> AddHallSchemeAsync(AddHallSchemeRequest request, CancellationToken ct);

    Task<Empty> RemoveHallSchemeAsync(RemoveHallSchemeRequest request, CancellationToken ct);

    Task<HallSchemeResponse> GetHallSchemeByIdAsync(GetHallSchemeByIdRequest request, CancellationToken ct);

    Task<HallSchemesList> GetVenueHallSchemesAsync(GetVenueHallSchemesRequest request, CancellationToken ct);

    Task<VenueHasAvailableSchemeResponse> VenueHasAvailableSchemeAsync(
        VenueHasAvailableSchemeRequest request,
        CancellationToken ct);
}