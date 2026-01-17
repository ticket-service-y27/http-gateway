namespace HttpGateway.Models.Tickets.Promocodes;

public record CreatePromocodeDto(string Code, long DiscountPercentage, long Count);