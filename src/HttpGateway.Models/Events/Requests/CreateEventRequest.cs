namespace HttpGateway.Models.Events.Requests;

public class CreateEventRequest
{
    public long OrganizerId { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public long CategoryId { get; init; }

    public long VenueId { get; init; }
}