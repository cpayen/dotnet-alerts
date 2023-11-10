namespace Common.Contracts;

public record AlertCreated(DateTimeOffset RaisedAt, int Level);