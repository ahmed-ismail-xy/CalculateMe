namespace CalculateMe.Result.Domain.Abstractions.Contracts.Events;

public sealed record CalculationProcessed(
    Guid CalculationId,
    decimal Result,
    DateTime ProcessedAt);
