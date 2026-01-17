namespace HttpGateway.Models.Tickets.Tickets;

public record TicketDto(
    long Id,
    long UserId,
    long EventId,
    long Row,
    long Number,
    long Price,
    long PaymentId,
    string? Status,
    DateTimeOffset Created,
    string? AppliedPromocode);
