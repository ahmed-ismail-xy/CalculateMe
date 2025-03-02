namespace CalculateMe.Calculation.Api.Models;

public sealed record CalculationResponse(
    Guid Id,
    decimal FirstNumber,
    decimal SecondNumber,
    decimal Result,
    string Status,
    DateTime CreatedAt,
    DateTime? CompletedAt);