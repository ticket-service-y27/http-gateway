using HttpGateway.Models.Seats.SeatDtos;

namespace HttpGateway.Models.Seats.Requests;

public class BookSeatRequest
{
    public long HallSchemeId { get; set; }

    public ICollection<SeatInfoDto> Seats { get; init; } = new List<SeatInfoDto>();
}