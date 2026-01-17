using TicketService.Grpc.Tickets;

namespace HttpGateway.Clients.GrpcClients;

public class TicketServiceGrpcClient : ITicketServiceGrpcClient
{
    private readonly TicketsService.TicketsServiceClient _tickets;

    public TicketServiceGrpcClient(TicketsService.TicketsServiceClient tickets)
    {
        _tickets = tickets;
    }

    public async Task<long> CreateTicketAsync(
        long userId,
        long eventId,
        long price,
        long row,
        long number,
        CancellationToken cancellationToken,
        string? appliedPromocode)
    {
        var req = new CreateTicketRequest
        {
            UserId = userId,
            EventId = eventId,
            Price = price,
            Row = row,
            Number = number,
            AppliedPromocode = appliedPromocode ?? string.Empty,
        };

        CreateTicketResponse resp = await _tickets.CreateTicketAsync(req, cancellationToken: cancellationToken);
        return resp.TicketId;
    }

    public Task<PayTicketResponse> PayTicketAsync(long ticketId, CancellationToken cancellationToken)
    {
        var req = new PayTicketRequest { TicketId = ticketId };
        return _tickets.PayTicketAsync(req, cancellationToken: cancellationToken).ResponseAsync;
    }

    public Task<GetUserTicketsResponse> GetUserTicketsAsync(long userId, long? cursor, CancellationToken cancellationToken)
    {
        var req = new GetUserTicketsRequest
        {
            UserId = userId,
            Cursor = cursor ?? 0,
        };

        return _tickets.GetUserTicketsAsync(req, cancellationToken: cancellationToken).ResponseAsync;
    }

    public Task<RefundTicketsResponse> RefundTicketsAsync(long ticketId, long schemeId, CancellationToken cancellationToken)
    {
        var req = new RefundTicketsRequest
        {
            TicketId = ticketId,
            SchemeId = schemeId,
        };

        return _tickets.RefundTicketsAsync(req, cancellationToken: cancellationToken).ResponseAsync;
    }
}