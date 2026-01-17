using EventService.Presentation.Grpc;
using HttpGateway.Clients.Abstractions;
using HttpGateway.Models.Seats.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HttpGateway.Controllers;

[ApiController]
[Route("api/seats")]
public sealed class SeatController : ControllerBase
{
    private readonly ISeatValidationClientGrpc _seatClient;

    public SeatController(ISeatValidationClientGrpc seatClient)
    {
        _seatClient = seatClient;
    }

    [HttpPost("exists")]
    [Authorize]
    [ProducesResponseType(typeof(SeatExistsResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<SeatExistsResponse>> SeatExists([FromBody] SeatRequest request, CancellationToken ct)
    {
        if (request == null) return BadRequest();

        var grpcRequest = new SeatExistsRequest
        {
            HallSchemeId = request.HallSchemeId,
            Row = request.Row,
            SeatNumber = request.SeatNumber,
        };

        SeatExistsResponse response = await _seatClient.SeatExistsAsync(grpcRequest, ct);
        return Ok(response);
    }

    [HttpPost("available")]
    [Authorize]
    [ProducesResponseType(typeof(SeatAvailableResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<SeatAvailableResponse>> IsSeatAvailable([FromBody] SeatRequest request, CancellationToken ct)
    {
        if (request == null) return BadRequest();

        var grpcRequest = new IsSeatAvailableRequest
        {
            HallSchemeId = request.HallSchemeId,
            Row = request.Row,
            SeatNumber = request.SeatNumber,
        };

        SeatAvailableResponse response = await _seatClient.IsSeatAvailableAsync(grpcRequest, ct);
        return Ok(response);
    }

    [HttpPost("status")]
    [Authorize]
    [ProducesResponseType(typeof(SeatStatusResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<SeatStatusResponse>> GetSeatStatus([FromBody] SeatRequest request, CancellationToken ct)
    {
        if (request == null) return BadRequest();

        var grpcRequest = new GetSeatStatusRequest
        {
            HallSchemeId = request.HallSchemeId,
            Row = request.Row,
            SeatNumber = request.SeatNumber,
        };

        SeatStatusResponse response = await _seatClient.GetSeatStatusAsync(grpcRequest, ct);
        return Ok(response);
    }

    [HttpPost("book")]
    [Authorize]
    [ProducesResponseType(typeof(BookSeatsResponse), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public async Task<ActionResult<BookSeatsResponse>> BookSeats([FromBody] BookSeatRequest request, CancellationToken ct)
    {
        if (request == null || request.Seats.Count == 0) return BadRequest();

        var grpcRequest = new BookSeatsRequest
        {
            HallSchemeId = request.HallSchemeId,
        };
        grpcRequest.Seats.AddRange(request.Seats.Select(s => new SeatInfo
        {
            Row = s.Row,
            SeatNumber = s.SeatNumber,
        }));

        BookSeatsResponse response = await _seatClient.BookSeatsAsync(grpcRequest, ct);
        if (!response.Success) return Conflict(response);

        return Ok(response);
    }

    [HttpPost("return")]
    [Authorize]
    [ProducesResponseType(typeof(ReturnSeatsResponse), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public async Task<ActionResult<ReturnSeatsResponse>> ReturnSeats([FromBody] ReturnSeatRequest request, CancellationToken ct)
    {
        if (request == null || request.Seats.Count == 0) return BadRequest();

        var grpcRequest = new ReturnSeatsRequest
        {
            HallSchemeId = request.HallSchemeId,
        };
        grpcRequest.Seats.AddRange(request.Seats.Select(s => new SeatInfo
        {
            Row = s.Row,
            SeatNumber = s.SeatNumber,
        }));

        ReturnSeatsResponse response = await _seatClient.ReturnSeatsAsync(grpcRequest, ct);
        if (!response.Success) return Conflict(response);

        return Ok(response);
    }
}