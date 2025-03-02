namespace CalculateMe.Calculation.Domain.Abstractions.Contracts.Events;

public sealed record CalculationProcessed(
    Guid CalculationId,
    decimal Result,
    DateTime ProcessedAt);
