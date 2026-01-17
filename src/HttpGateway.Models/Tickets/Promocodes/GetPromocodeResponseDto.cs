namespace HttpGateway.Models.Tickets.Promocodes;

public record GetPromocodeResponseDto(long Id, string Code, long DiscountPercentage, long Count);