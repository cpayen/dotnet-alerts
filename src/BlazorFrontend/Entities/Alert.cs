namespace BlazorFrontend.Entities;

public class Alert
{
    public Guid Id { get; set; }
    public DateTimeOffset RaisedAt { get; set; }
    public AlertLevel Level { get; set; }
}