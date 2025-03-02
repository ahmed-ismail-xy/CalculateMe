namespace CalculateMe.Result.Domain.Entities;

public sealed class TotalResult
{
    public Guid Id { get; private set; }
    public decimal Result { get; private set; } = 0;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private TotalResult() { }

    public static TotalResult Create(Guid id, decimal result)
    {
        return new TotalResult
        {
            Id = id,
            Result = result,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Calculate(decimal number)
    {
        Result += number;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reset()
    {
        Result = 0;
        UpdatedAt = DateTime.UtcNow;
    }
}
