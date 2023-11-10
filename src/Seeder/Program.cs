using Common.MassTransit;
using Seeder;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransitWithRabbitMq();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();