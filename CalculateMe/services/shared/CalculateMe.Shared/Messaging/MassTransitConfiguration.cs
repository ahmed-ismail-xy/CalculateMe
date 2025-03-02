using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CalculateMe.Shared.Messaging;

public static class MassTransitConfiguration
{
    public static IServiceCollection AddMassTransitWithRabbitMQ<TDbContext>(
        this IServiceCollection services,
        string rabbitMqHost = "localhost",
        string rabbitMqUsername = "guest",
        string rabbitMqPassword = "guest",
        Action<IBusRegistrationConfigurator>? configureConsumers = null)
        where TDbContext : DbContext
    {
        services.AddMassTransit(x =>
        {
            x.AddEntityFrameworkOutbox<TDbContext>(o =>
            {
                o.UseSqlServer();
                o.UseBusOutbox();
                o.DuplicateDetectionWindow = TimeSpan.FromHours(1);
                o.QueryDelay = TimeSpan.FromSeconds(5);
            });

            configureConsumers?.Invoke(x);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqHost, "/", h =>
                {
                    h.Username(rabbitMqUsername);
                    h.Password(rabbitMqPassword);
                });

                cfg.UseMessageRetry(r => r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2)));

                cfg.ReceiveEndpoint("outbox-queue", e =>
                {
                    e.UseEntityFrameworkOutbox<TDbContext>(context);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
