using HttpGateway.Clients;
using HttpGateway.Models.Tickets.Tickets;
using Microsoft.AspNetCore.Mvc;
using TicketService.Grpc.Tickets;

namespace HttpGateway.Controllers;

[ApiController]
[Route("gateway/tickets")]
public class TicketsController : ControllerBase
{
    private readonly ITicketServiceGrpcClient _client;

    public TicketsController(ITicketServiceGrpcClient client)
    {
        _client = client;
    }

    [HttpPost]
    public async Task<ActionResult<CreateTicketResponseDto>> Create(
        [FromBody] CreateTicketDto dto,
        CancellationToken ct)
    {
        long id = await _client.CreateTicketAsync(
            dto.UserId,
            dto.EventId,
            dto.Price,
            dto.Row,
            dto.Number,
            ct,
            dto.AppliedPromocode);

        return Ok(new CreateTicketResponseDto(id));
    }

    [HttpPost("{ticketId:long}/pay")]
    public async Task<ActionResult<PayTicketResponseDto>> Pay([FromRoute] long ticketId, CancellationToken ct)
    {
        PayTicketResponse resp = await _client.PayTicketAsync(ticketId, ct);

        return Ok(new PayTicketResponseDto(
            resp.Success,
            string.IsNullOrWhiteSpace(resp.FailReason) ? null : resp.FailReason));
    }

    [HttpGet("users/{userId:long}")]
    public async Task<ActionResult<GetUserTicketsDto>> GetUserTickets(
        [FromRoute] long userId,
        [FromQuery] long? cursor,
        CancellationToken ct)
    {
        GetUserTicketsResponse resp = await _client.GetUserTicketsAsync(userId, cursor, ct);

        var tickets = resp.Tickets.Select(t => new TicketDto(
                t.Id,
                t.UserId,
                t.EventId,
                t.SeatRow,
                t.SeatNumber,
                t.Price,
                t.PaymentId,
                t.Status.ToString(),
                t.CreatedAt.ToDateTimeOffset(),
                string.IsNullOrWhiteSpace(t.AppliedPromocode) ? null : t.AppliedPromocode))
            .ToList();

        return Ok(new GetUserTicketsDto(tickets));
    }

    [HttpPost("{ticketId:long}/refund")]
    public async Task<ActionResult<RefundTicketResponseDto>> Refund(
        [FromRoute] long ticketId,
        [FromBody] RefundTicketDto dto,
        CancellationToken ct)
    {
        RefundTicketsResponse resp = await _client.RefundTicketsAsync(ticketId, dto.SchemeId, ct);
        return Ok(new RefundTicketResponseDto(resp.Success));
    }
}