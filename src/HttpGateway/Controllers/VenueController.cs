using EventService.Presentation.Grpc;
using HttpGateway.Clients.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HttpGateway.Controllers;

[ApiController]
[Route("api/venues")]
public sealed class VenueController : ControllerBase
{
    private readonly IVenueManagementClientGrpc _venueClient;

    public VenueController(IVenueManagementClientGrpc venueClient)
    {
        _venueClient = venueClient;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(VenueResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VenueResponse>> CreateVenue(
        [FromBody] Models.Venues.CreateVenueRequest dto,
        CancellationToken ct)
    {
        var request = new CreateVenueRequest
        {
            Name = dto.Name,
            Address = dto.Address,
        };

        VenueResponse response = await _venueClient.CreateVenueAsync(request, ct);
        return Ok(response);
    }

    [HttpPut("{venueId}")]
    [Authorize]
    [ProducesResponseType(typeof(VenueResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VenueResponse>> UpdateVenue(
        long venueId,
        [FromBody] Models.Venues.UpdateVenueRequest dto,
        CancellationToken ct)
    {
        var request = new UpdateVenueRequest
        {
            VenueId = venueId,
            Name = dto.Name,
            Address = dto.Address,
        };

        VenueResponse response = await _venueClient.UpdateVenueAsync(request, ct);
        return Ok(response);
    }

    [HttpPost("{venueId}/halls")]
    [Authorize]
    [ProducesResponseType(typeof(HallSchemeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HallSchemeResponse>> AddHallScheme(
        long venueId,
        [FromBody] Models.Venues.AddHallSchemeRequest dto,
        CancellationToken ct)
    {
        var request = new AddHallSchemeRequest
        {
            VenueId = venueId,
            SchemeName = dto.SchemeName,
            Rows = dto.Rows,
            Columns = dto.Columns,
        };

        HallSchemeResponse response = await _venueClient.AddHallSchemeAsync(request, ct);
        return Ok(response);
    }

    [HttpDelete("halls/{hallSchemeId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveHallScheme(
        long hallSchemeId,
        CancellationToken ct)
    {
        var request = new RemoveHallSchemeRequest
        {
            HallSchemeId = hallSchemeId,
        };

        await _venueClient.RemoveHallSchemeAsync(request, ct);
        return NoContent();
    }

    [HttpGet("halls/{hallSchemeId}")]
    [Authorize]
    [ProducesResponseType(typeof(HallSchemeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HallSchemeResponse>> GetHallSchemeById(
        long hallSchemeId,
        CancellationToken ct)
    {
        var request = new GetHallSchemeByIdRequest
        {
            HallSchemeId = hallSchemeId,
        };

        HallSchemeResponse response = await _venueClient.GetHallSchemeByIdAsync(request, ct);
        return Ok(response);
    }

    [HttpGet("{venueId}/halls")]
    [Authorize]
    [ProducesResponseType(typeof(HallSchemesList), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HallSchemesList>> GetVenueHallSchemes(
        long venueId,
        CancellationToken ct)
    {
        var request = new GetVenueHallSchemesRequest
        {
            VenueId = venueId,
        };

        HallSchemesList response = await _venueClient.GetVenueHallSchemesAsync(request, ct);
        return Ok(response);
    }

    [HttpGet("{venueId}/available")]
    [Authorize]
    [ProducesResponseType(typeof(VenueHasAvailableSchemeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VenueHasAvailableSchemeResponse>> VenueHasAvailableScheme(long venueId, CancellationToken ct)
    {
        var request = new VenueHasAvailableSchemeRequest
        {
            VenueId = venueId,
        };

        VenueHasAvailableSchemeResponse response = await _venueClient.VenueHasAvailableSchemeAsync(request, ct);
        return Ok(response);
    }
}