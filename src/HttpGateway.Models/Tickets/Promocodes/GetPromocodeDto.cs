namespace HttpGateway.Models.Tickets.Promocodes;

public record GetPromocodeDto(long Id, string Code, long DiscountPercentage, long Count);