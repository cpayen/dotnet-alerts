using Common.Abstractions;
using Common.Contracts;
using MassTransit;
using WebApi.Entities;

namespace WebApi.Consumers;

public class AlertCreatedConsumer : IConsumer<AlertCreated>
{
    private readonly IRepository<Alert> _repository;
    private readonly ILogger<AlertCreatedConsumer> _logger;

    public AlertCreatedConsumer(ILogger<AlertCreatedConsumer> logger, IRepository<Alert> repository)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<AlertCreated> context)
    {
        var message = context.Message;
        _logger.LogInformation(
            "Consuming alert: {RaisedAt:T}, level {Level}", 
            message.RaisedAt, message.Level);
        
        var item = new Alert()
        {
            Id = Guid.NewGuid(),
            RaisedAt = message.RaisedAt,
            Level = (AlertLevel)message.Level
        };

        await _repository.CreateAsync(item);
    }
}