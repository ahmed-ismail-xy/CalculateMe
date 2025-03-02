using CalculateMe.Calculation.Domain.Abstractions.Contracts.Events;
using CalculateMe.Calculation.Domain.Abstractions;
using CalculateMe.Calculation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MassTransit;

namespace CalculateMe.Calculation.Application.Services;

public sealed class CalculationService : ICalculationService
{
    private readonly ICalculationDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CalculationService> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public CalculationService(
        ICalculationDbContext dbContext,
        IUnitOfWork unitOfWork,
        ILogger<CalculationService> logger,
        IPublishEndpoint publishEndpoint)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<SummationTask> CalculateAsync(
        decimal firstNumber,
        decimal secondNumber,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Calculating {FirstNumber} + {SecondNumber}", firstNumber, secondNumber);

        var calculation = SummationTask.Create(
            Guid.NewGuid(),
            firstNumber,
            secondNumber);

        decimal result = firstNumber + secondNumber;

        calculation.Complete(result);

        _dbContext.SummationTasks.Add(calculation);

        await _publishEndpoint.Publish(new CalculationProcessed(
            calculation.Id,
            result,
            DateTime.UtcNow), cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Calculation {CalculationId} saved", calculation.Id);

        return calculation;
    }

    public async Task<SummationTask?> GetCalculationAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting calculation {CalculationId}", id);

        return await _dbContext.SummationTasks
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
