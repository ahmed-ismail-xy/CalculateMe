using CalculateMe.Calculation.Application.Consumers;
using CalculateMe.Calculation.Domain.Abstractions;
using CalculateMe.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CalculateMe.Calculation.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitMqHost = configuration["RabbitMq:Host"] ?? throw new ArgumentNullException(nameof(configuration), "RabbitMq:Host is not configured");
        var rabbitMqUsername = configuration["RabbitMq:Username"] ?? throw new ArgumentNullException(nameof(configuration), "RabbitMq:Username is not configured");
        var rabbitMqPassword = configuration["RabbitMq:Password"] ?? throw new ArgumentNullException(nameof(configuration), "RabbitMq:Password is not configured");

        services.AddDbContext<CalculationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddMassTransitWithRabbitMQ<CalculationDbContext>(
            rabbitMqHost,
            rabbitMqUsername,
            rabbitMqPassword,
            x =>
            {
                x.AddConsumer<CalculationFailedConsumer>();
            });

        services.AddScoped<ICalculationDbContext, CalculationDbContext>();
        services.AddScoped<IUnitOfWork, CalculationDbContext>();

        return services;
    }
}
