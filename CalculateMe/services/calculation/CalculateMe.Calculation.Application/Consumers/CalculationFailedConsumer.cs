using CalculateMe.Calculation.Domain.Abstractions.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CalculateMe.Calculation.Application.Consumers;

public class CalculationFailedConsumer : IConsumer<CalculationFailed>
{
    private readonly ILogger<CalculationFailedConsumer> _logger;

    public CalculationFailedConsumer(ILogger<CalculationFailedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CalculationFailed> context)
    {
        _logger.LogWarning("Calculation {CalculationId} failed: {Reason}", context.Message.CalculationId, context.Message.Reason);

        // could trigger alerts, notifications, etc.

        return Task.CompletedTask;
    }
}
