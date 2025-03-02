namespace CalculateMe.Calculation.Domain.Abstractions.Contracts.Events;

public sealed record CalculationFailed(
    Guid CalculationId,
    string Reason,
    DateTime FailedAt);