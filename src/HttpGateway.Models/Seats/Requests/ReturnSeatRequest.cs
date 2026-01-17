using HttpGateway.Models.Seats.SeatDtos;

namespace HttpGateway.Models.Seats.Requests;

public class ReturnSeatRequest
{
    public long HallSchemeId { get; set; }

    public ICollection<SeatInfoDto> Seats { get; init; } = new List<SeatInfoDto>();
}