using Common.Abstractions;
using Common.Contracts;
using MassTransit;
using WebApi.Entities;
using WebApi.SignalR;

namespace WebApi.Consumers;

public class AlertCreatedConsumer : IConsumer<AlertCreated>
{
    private readonly IRepository<Alert> _repository;
    private readonly MessageHub _hub;
    private readonly ILogger<AlertCreatedConsumer> _logger;

    public AlertCreatedConsumer(ILogger<AlertCreatedConsumer> logger, MessageHub hub, IRepository<Alert> repository)
    {
        _repository = repository;
        _hub = hub;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<AlertCreated> context)
    {
        var message = context.Message;
        _logger.LogInformation(
            "Consuming alert: {RaisedAt:T}, level {Level}", 
            message.RaisedAt, message.Level);
        
        var alert = new Alert()
        {
            Id = Guid.NewGuid(),
            RaisedAt = message.RaisedAt,
            Level = (AlertLevel)message.Level
        };

        await _hub.SendAlertsync(alert);
        await _repository.CreateAsync(alert);
    }
}