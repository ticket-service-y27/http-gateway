using TicketService.Grpc.Tickets;

namespace HttpGateway.Clients;

public interface ITicketServiceGrpcClient
{
    Task<long> CreateTicketAsync(
         long userId,
         long eventId,
         long price,
         long row,
         long number,
         CancellationToken cancellationToken,
         string? appliedPromocode);

    Task<PayTicketResponse> PayTicketAsync(long ticketId, CancellationToken cancellationToken);

    Task<GetUserTicketsResponse> GetUserTicketsAsync(long userId, long? cursor, CancellationToken cancellationToken);

    Task<RefundTicketsResponse> RefundTicketsAsync(long ticketId, long schemeId, CancellationToken cancellationToken);
}