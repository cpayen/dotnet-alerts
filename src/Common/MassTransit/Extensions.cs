using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
    {
        services.AddMassTransit(configure =>
        {
            configure.UsingRabbitMq((context, cfg) =>
            {
                var configuration = context.GetService<IConfiguration>();
                var rabbitMqSettings =
                    configuration?.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>()
                    ?? throw new InvalidOperationException("Invalid Rabbit MQ configuration");

                Console.WriteLine(rabbitMqSettings.Host);

                cfg.Host(rabbitMqSettings.Host, rabbitMqSettings.VirtualHost, h =>
                {
                    h.Username(rabbitMqSettings.Username);
                    h.Password(rabbitMqSettings.Password);
                });

                cfg.ConfigureEndpoints(context);
                cfg.UseMessageRetry((retryConfigurator) => 
                    retryConfigurator.Interval(3, TimeSpan.FromSeconds(5)));
            });

            configure.AddConsumers(Assembly.GetEntryAssembly());
        });

        return services;
    }
}