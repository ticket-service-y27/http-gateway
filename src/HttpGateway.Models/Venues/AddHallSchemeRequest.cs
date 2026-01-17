namespace HttpGateway.Models.Venues;

public class AddHallSchemeRequest
{
    public string? SchemeName { get; set; }

    public int Rows { get; set; }

    public int Columns { get; set; }
}