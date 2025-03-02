using System.Text.Json.Serialization;

namespace CalculateMe.Calculation.Domain.Entities;

public sealed class SummationTask
{
    public Guid Id { get; private set; }
    public decimal FirstNumber { get; private set; }
    public decimal SecondNumber { get; private set; }
    public decimal Result { get; private set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SummationTaskStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private SummationTask() { }

    public static SummationTask Create(Guid id, decimal firstNumber, decimal secondNumber)
    {
        return new SummationTask
        {
            Id = id,
            FirstNumber = firstNumber,
            SecondNumber = secondNumber,
            Status = SummationTaskStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Complete(decimal result)
    {
        Result = result;
        Status = SummationTaskStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public void MarkAsProcessing()
    {
        Status = SummationTaskStatus.Processing;
    }

    public void MarkAsFailed()
    {
        Status = SummationTaskStatus.Failed;
        CompletedAt = DateTime.UtcNow;
    }
}
