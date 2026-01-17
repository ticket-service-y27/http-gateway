namespace HttpGateway.Models.Seats.Requests;

public class SeatRequest
{
    public long HallSchemeId { get; set; }

    public int Row { get; set; }

    public int SeatNumber { get; set; }
}