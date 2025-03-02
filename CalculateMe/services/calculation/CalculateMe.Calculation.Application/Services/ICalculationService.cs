using CalculateMe.Calculation.Domain.Entities;

namespace CalculateMe.Calculation.Application.Services;

public interface ICalculationService
{
    Task<SummationTask> CalculateAsync(
        decimal firstNumber,
        decimal secondNumber,
        CancellationToken cancellationToken = default);

    Task<SummationTask?> GetCalculationAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
