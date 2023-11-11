namespace Common.MongoDb;

public class MongoDbSettings
{
    public string Host { get; init; } = default!;
    public int Port { get; init; }
    public string DatabaseName { get; init; } = default!;
    
    public string ConnectionString => $"mongodb://{Host}:{Port}";
}