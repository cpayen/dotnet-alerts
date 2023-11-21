using Common.Abstractions;
using Common.Contracts;
using MassTransit;
using WebApi.Entities;
using WebApi.SignalR;

namespace WebApi.Consumers;

public class AlertCreatedConsumer(ILogger<AlertCreatedConsumer> logger, MessageHub hub, IRepository<Alert> repository)
    : IConsumer<AlertCreated>
{
    public async Task Consume(ConsumeContext<AlertCreated> context)
    {
        var message = context.Message;
        logger.LogInformation(
            "Consuming alert: {RaisedAt:T}, level {Level}", 
            message.RaisedAt, message.Level);
        
        var alert = new Alert()
        {
            Id = Guid.NewGuid(),
            RaisedAt = message.RaisedAt,
            Level = (AlertLevel)message.Level
        };

        await hub.SendAlertAsync(alert);
        await repository.CreateAsync(alert);
    }
}