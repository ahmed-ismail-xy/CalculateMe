using CalculateMe.Result.Application.Services;
using CalculateMe.Result.Domain.Abstractions.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CalculateMe.Result.Application.Consumers;

public class CalculationProcessedConsumer : IConsumer<CalculationProcessed>
{
    private readonly ILogger<CalculationProcessedConsumer> _logger;
    private readonly IResultService _resultService;

    public CalculationProcessedConsumer(
        ILogger<CalculationProcessedConsumer> logger,
        IResultService resultService)
    {
        _logger = logger;
        _resultService = resultService;
    }

    public async Task Consume(ConsumeContext<CalculationProcessed> context)
    {
        _logger.LogInformation("Processing calculation {CalculationId} with result: {Result}", context.Message.CalculationId, context.Message.Result);

        await _resultService.CalculateAsync(context.Message.Result, context.CancellationToken);

        _logger.LogInformation("Calculation {CalculationId} processed successfully with result: {Result}", context.Message.CalculationId, context.Message.Result);
    }
}
