namespace HttpGateway.Models.Tickets.Tickets;

public record CreateTicketDto(long UserId, long EventId, long Price, long Row, long Number, string? AppliedPromocode);