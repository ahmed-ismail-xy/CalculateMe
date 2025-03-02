using Microsoft.Extensions.DependencyInjection;
using CalculateMe.Result.Application.Consumers;
using CalculateMe.Result.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using CalculateMe.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace CalculateMe.Result.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitMqHost = configuration["RabbitMq:Host"] ?? throw new ArgumentNullException(nameof(configuration), "RabbitMq:Host is not configured");
        var rabbitMqUsername = configuration["RabbitMq:Username"] ?? throw new ArgumentNullException(nameof(configuration), "RabbitMq:Username is not configured");
        var rabbitMqPassword = configuration["RabbitMq:Password"] ?? throw new ArgumentNullException(nameof(configuration), "RabbitMq:Password is not configured");

        services.AddDbContext<ResultDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddMassTransitWithRabbitMQ<ResultDbContext>(
            rabbitMqHost,
            rabbitMqUsername,
            rabbitMqPassword,
            x =>
            {
                x.AddConsumer<CalculationProcessedConsumer>();
            });

        services.AddScoped<IResultDbContext, ResultDbContext>();
        services.AddScoped<IUnitOfWork, ResultDbContext>();

        return services;
    }
}
