namespace HttpGateway.Models.Events.Requests;

public class UpdateEventRequest
{
    public long OrganizerId { get; init; }

    public string? Title { get; init; }

    public string? Description { get; init; }

    public DateTime? StartDate { get; init; }

    public DateTime? EndDate { get; init; }

    public long CategoryId { get; init; }

    public long VenueId { get; init; }
}