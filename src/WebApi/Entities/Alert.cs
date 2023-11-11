using Common.Abstractions;

namespace WebApi.Entities;

public class Alert : IEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset RaisedAt { get; set; }
    public AlertLevel Level { get; set; }
}