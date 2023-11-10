using Common.Contracts;
using MassTransit;

namespace Seeder;

public class Worker : BackgroundService
{
    private readonly IBus _bus;
    private readonly ILogger<Worker> _logger;
    private readonly Random _random;

    public Worker(IBus bus, ILogger<Worker> logger)
    {
        _bus = bus;
        _logger = logger;
        _random = new Random();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var level = _random.Next(1, 4);
            var alert = new AlertCreated(DateTimeOffset.Now, level);

            _logger.LogInformation("Publishing alert: {RaisedAt:T}, level {Level}", alert.RaisedAt, alert.Level);
            await _bus.Publish(alert);

            var timer = Convert.ToInt32(_random.NextDouble() * 3_000);
            await Task.Delay(timer, stoppingToken);
        }
    }
}