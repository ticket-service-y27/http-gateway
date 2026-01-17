using EventService.Presentation.Grpc;
using Google.Protobuf.WellKnownTypes;
using HttpGateway.Clients.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HttpGateway.Controllers;

[ApiController]
[Route("api/events")]
public sealed class EventController : ControllerBase
{
    private readonly IEventManagerClientGrpc _eventClient;

    public EventController(IEventManagerClientGrpc eventClient)
    {
        _eventClient = eventClient;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EventResponse>> Create(
        [FromBody] Models.Events.Requests.CreateEventRequest request,
        CancellationToken ct)
    {
        var grpcRequest = new CreateEventRequest
        {
            OrganizerId = request.OrganizerId,
            Title = request.Title,
            Description = request.Description,
            StartDate = Timestamp.FromDateTime(request.StartDate.ToUniversalTime()),
            EndDate = Timestamp.FromDateTime(request.EndDate.ToUniversalTime()),
            CategoryId = request.CategoryId,
            VenueId = request.VenueId,
        };

        EventResponse response = await _eventClient.CreateEventAsync(grpcRequest, ct);
        return Ok(response);
    }

    [HttpPut("{eventId}")]
    [Authorize]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EventResponse>> Update(
        long eventId,
        [FromBody] Models.Events.Requests.UpdateEventRequest request,
        CancellationToken ct)
    {
        var grpcRequest = new UpdateEventRequest
        {
            OrganizerId = request.OrganizerId,
            EventId = eventId,
            Title = request.Title ?? string.Empty,
            Description = request.Description ?? string.Empty,
            StartDate = request.StartDate is null
                ? new Timestamp()
                : Timestamp.FromDateTime(request.StartDate.Value.ToUniversalTime()),
            EndDate = request.EndDate is null
                ? new Timestamp()
                : Timestamp.FromDateTime(request.EndDate.Value.ToUniversalTime()),
            CategoryId = request.CategoryId,
            VenueId = request.VenueId,
        };

        EventResponse response = await _eventClient.UpdateEventAsync(grpcRequest, ct);
        return Ok(response);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<EventResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<EventResponse>>> GetAll(CancellationToken ct)
    {
        try
        {
            var events = new List<EventResponse>();

            await foreach (EventResponse ev in _eventClient.GetAllEventsAsync(ct))
            {
                events.Add(ev);
            }

            return Ok(events);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}