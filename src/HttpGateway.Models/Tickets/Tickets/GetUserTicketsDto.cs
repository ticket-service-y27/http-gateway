namespace HttpGateway.Models.Tickets.Tickets;

public record GetUserTicketsDto(IReadOnlyList<TicketDto> Tickets);